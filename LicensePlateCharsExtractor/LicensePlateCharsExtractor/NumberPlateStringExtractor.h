#pragma once

#include "opencv2/core/core.hpp"
#include "opencv2/imgproc/imgproc.hpp"
#include "opencv2/highgui/highgui.hpp"
#include <opencv2/ml/ml.hpp>
#include <math.h>
#include <string.h>

using namespace cv;
using namespace std;

const int RESIZED_IMAGE_WIDTH = 20;
const int RESIZED_IMAGE_HEIGHT = 30;

class NumberPlateStringExtractor
{
public:
	NumberPlateStringExtractor(string classFileName, string trainingDataFileName)
	{
		NumberPlateStringExtractor::classFile = classFileName;
		NumberPlateStringExtractor::trainingDataFile = trainingDataFileName;
	}
private:
	string classFile, trainingDataFile;
public:
	string Extract(Mat& mat);
};
