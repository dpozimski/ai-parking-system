#pragma once

#include "opencv2/imgproc/imgproc.hpp"

using namespace cv;

class MatImageManager
{
public:
	Mat Create(std::string imagePath);
	void SaveToFile(Mat& mat, std::string outputPath);
	void Show(Mat& mat);
};