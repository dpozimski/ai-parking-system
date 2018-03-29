#include "NumberPlateRectangleExtractor.h"

Mat NumberPlateRectangleExtractor::Extract(Mat & image)
{
	FindSquares(image, squares);
	if (squares.size() == 1)
		squares.push_back(squares[0]);
	Mat& mat = DrawSquares(image, squares);
	return RemoveBlueRectangle(mat);
}

double NumberPlateRectangleExtractor::Angle(Point pt1, Point pt2, Point pt0) {
	double dx1 = pt1.x - pt0.x;
	double dy1 = pt1.y - pt0.y;
	double dx2 = pt2.x - pt0.x;
	double dy2 = pt2.y - pt0.y;
	return (dx1*dx2 + dy1*dy2) / sqrt((dx1*dx1 + dy1*dy1)*(dx2*dx2 + dy2*dy2) + 1e-10); // zwraca cosinus pomiedzy dwoma wektorami
}

double NumberPlateRectangleExtractor::Proportion(Point pt1, Point pt2, Point pt0) {
	double dx1 = pt1.x - pt0.x;
	double dy1 = pt1.y - pt0.y;
	double dx2 = pt2.x - pt0.x;
	double dy2 = pt2.y - pt0.y;
	double lengh1 = sqrt(pow(dx1, 2) + pow(dy1, 2));
	double lengh2 = sqrt(pow(dx2, 2) + pow(dy2, 2));
	return lengh1 / lengh2; // zwraca proporcje d³ugosci dwóch wektorów
}

Mat NumberPlateRectangleExtractor::RemoveBlueRectangle(Mat& image)
{
	int startX = 25, startY = 0, width = image.cols, height = image.rows;
	Mat ROI(image, Rect(startX, startY, width - startX, height));
	Mat croppedImage;
	ROI.copyTo(croppedImage);
	//DebugPreview(croppedImage, "PostCropped");
	return croppedImage;
}

// returns sequence of squares detected on the image.
// the sequence is stored in the specified memory storage
void NumberPlateRectangleExtractor::FindSquares(const Mat& image, vector<vector<Point> >& squares)
{
	squares.clear();

	Mat timg(image);
	Mat gray0(timg.size(), CV_8U), gray;

	vector<vector<Point> > contours;
	int ch[] = { 0, 0 };
	mixChannels(&timg, 1, &gray0, 1, ch, 1);

	// try several threshold levels
	for (int l = 0; l < tresholdLevels; l++)
	{
		// hack: use Canny instead of zero threshold level.
		// Canny helps to catch squares with gradient shading
		if (l == 0)
		{
			// apply Canny. Take the upper threshold from slider
			// and set the lower to 0 (which forces edges merging)
			Canny(gray0, gray, 5, thresh, 5);
			// dilate canny output to remove potential
			// holes between edge segments
			dilate(gray, gray, Mat(), Point(-1, -1));
		}
		else
		{
			// apply threshold if l!=0:
			//     tgray(x,y) = gray(x,y) < (l+1)*255/N ? 255 : 0
			gray = gray0 >= (l + 1) * 255 / tresholdLevels;
		}

		// find contours and store them all as a list
		findContours(gray, contours, RETR_LIST, CHAIN_APPROX_SIMPLE);

		vector<Point> approx;
		// test each contour
		for (size_t i = 0; i < contours.size(); i++)
		{
			// approximate contour with accuracy proportional
			// to the contour perimeter
			approxPolyDP(Mat(contours[i]), approx, arcLength(Mat(contours[i]), true)*0.02, true);

			// square contours should have 4 vertices after approximation
			// relatively large area (to filter out noisy contours)
			// and be convex.
			// Note: absolute value of an area is used because
			// area may be positive or negative - in accordance with the
			// contour orientation
			if (approx.size() == 4 &&
				fabs(contourArea(Mat(approx))) > 1000 &&
				isContourConvex(Mat(approx)))
			{
				double maxCosine = 0;

				for (int j = 2; j < 5; j++)
				{
					// find the maximum cosine of the angle between joint edges
					double cosine = fabs(Angle(approx[j % 4], approx[j - 2], approx[j - 1]));
					maxCosine = MAX(maxCosine, cosine);
				}

				// if cosines of all angles are small
				// (all angles are ~90 degree) then write quandrange
				// vertices to resultant sequence
				if ((maxCosine < 0.5) && (maxCosine > 0.01))//im mniejszy tym bardziej k¹t prosty musi byc------------------------------------
				{
					for (int j = 2; j < 5; j++)
					{
						// find the maximum cosine of the angle between joint edges
						double vectorLenghProportion = fabs(Proportion(approx[j % 4], approx[j - 2], approx[j - 1]));

						if (((vectorLenghProportion > 3.6) && (vectorLenghProportion < 5.6))) {
							squares.push_back(approx);
						}
					}

				}
			}
		}
	}
}

Mat NumberPlateRectangleExtractor::DrawSquares(Mat& image, const vector<vector<Point> >& squares)
{
	Mat output = Mat(57, 259, CV_8UC1, Scalar(0));

	for (size_t i = 0; i < squares.size(); i++)
	{
		const Point* p = &squares[i][0];

		int n = (int)squares[i].size();
		//dont detect the border
		if (p->x > 30 && p->y > 30)
			polylines(image, &p, &n, 1, true, Scalar(0, 255, 0), 1, LINE_AA);
		Mat mask = Mat(image.rows, image.cols, CV_8UC1, Scalar(0)); // twrzy obiekt maski
		Mat masked = Mat(image.rows, image.cols, CV_8UC1, Scalar(0)); // tworzy obraz po nalozeniu maski
		boundingRect(squares[i]); //tworzy otaczajacy obrys

								  // Create Polygon from vertices
		vector<Point> roi_poly;
		approxPolyDP(squares[1], roi_poly, 1.0, true);

		// Fill polygon white
		fillConvexPoly(mask, &roi_poly[0], (int)roi_poly.size(), 255, 8, 0);
		image.copyTo(masked, mask); // tworzy obraz z nalozona maska
		//DebugPreview(masked, "DetectedPlate"); //pokazuje go

		Rect boundRect;
		vector<vector<Point> > contours_poly(squares.size());
		double maxArea = 0.0;
		for (int i = 0; i < squares.size(); i++)
		{
			double area = contourArea(squares[i]);
			if (area > maxArea) {
				maxArea = area;
				approxPolyDP(Mat(squares[i]), contours_poly[i], 3, true);
				boundRect = boundingRect(Mat(contours_poly[i]));
			}
			Mat cropImage = masked(boundRect);
			//DebugPreview(cropImage, "CroppedPlate");

			// Input Quadilateral or Image plane coordinates
			Point2f inputQuad[4];
			// Output Quadilateral or World plane coordinates
			Point2f outputQuad[4];

			// Lambda Matrix
			Mat lambda(2, 4, CV_32FC1);
			//Input and Output Image;
			inputQuad[0] = (Point2f)squares[0][0];
			inputQuad[1] = (Point2f)squares[0][1];
			inputQuad[2] = (Point2f)squares[0][2];
			inputQuad[3] = (Point2f)squares[0][3];
			// The 4 points where the mapping is to be done , from top-left in clockwise order
			/*	outputQuad[0] = Point2f(0, 0);
			outputQuad[3] = Point2f(cropImage.cols - 1, 0);
			outputQuad[2] = Point2f(cropImage.cols - 1, cropImage.rows - 1);
			outputQuad[1] = Point2f(0, cropImage.rows - 1); */
			if (((squares[0][3].y) - (squares[0][1].y)) > 0) {
				outputQuad[1] = Point2f(0, 0);
				outputQuad[0] = Point2f(260 - 1, 0);
				outputQuad[3] = Point2f(260 - 1, 58 - 1);
				outputQuad[2] = Point2f(0, 58 - 1);
			}
			else {
				outputQuad[0] = Point2f(0, 0);
				outputQuad[3] = Point2f(260 - 1, 0);
				outputQuad[2] = Point2f(260 - 1, 58 - 1);
				outputQuad[1] = Point2f(0, 58 - 1);
			}

			// Get the Perspective Transform Matrix i.e. lambda 
			lambda = getPerspectiveTransform(inputQuad, outputQuad);
			// Apply the Perspective Transform just found to the src image
			warpPerspective(masked, output, lambda, output.size());
		}
	}

	return output;
}

void NumberPlateRectangleExtractor::DebugPreview(Mat & image, std::string previewName)
{
#if DEBUG
	imshow("Preview_" + previewName, image);
#endif
}
