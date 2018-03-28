#pragma once

#include "opencv2/core/core.hpp"
#include "opencv2/imgproc/imgproc.hpp"
#include "opencv2/highgui/highgui.hpp"
#include <math.h>
#include <string.h>

using namespace cv;
using namespace std;

class NumberPlateRectangleExtractor
{
public:
	NumberPlateRectangleExtractor(int thresh, int tresholdLevels)
	{
		NumberPlateRectangleExtractor::thresh = thresh;
		NumberPlateRectangleExtractor::tresholdLevels = tresholdLevels;
	}
public:
	Mat Extract(Mat& image);
private:
	vector<vector<Point> > squares;
	int thresh, tresholdLevels;
	double Angle(Point pt1, Point pt2, Point pt0);
	double Proportion(Point pt1, Point pt2, Point pt0);
	void FindSquares(const Mat& image, vector<vector<Point> >& squares);
	Mat DrawSquares(Mat& image, const vector<vector<Point> >& squares);
	void DebugPreview(Mat& image, std::string previewName);
	Mat RemoveBlueRectangle(Mat& image);
};
