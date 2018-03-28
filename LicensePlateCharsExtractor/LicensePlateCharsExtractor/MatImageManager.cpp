#include "MatImageManager.h"
#include "opencv2/core/core.hpp"
#include "opencv2/highgui/highgui.hpp"

Mat MatImageManager::Create(std::string imagePath)
{
	return imread(imagePath, 1);
}

void MatImageManager::SaveToFile(Mat & mat, std::string outputPath)
{
	imwrite(outputPath, mat);
}

void MatImageManager::Show(Mat & mat)
{
	imshow("Number plate", mat);
}
