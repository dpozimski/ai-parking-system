#include <iostream>
#include <math.h>
#include <string.h>
#include "opencv2/core/core.hpp"
#include "opencv2/imgproc/imgproc.hpp"
#include "opencv2/highgui/highgui.hpp"

#include "MatImageManager.h"
#include "NumberPlateRectangleExtractor.h"
#include "NumberPlateStringExtractor.h"

using namespace std;

const int thresh = 250, N = 5;
std::string classFileName = "classifications.xml", trainingDataFileName = "images.xml";

int main(int argc, char** argv)
{
	MatImageManager matImageManager;
	NumberPlateRectangleExtractor numberPlateRectExtractor(thresh, N);
	NumberPlateStringExtractor numberPlateStringExtractor(classFileName, trainingDataFileName);

	for (int i = 1; i < argc; i++)
	{
		string filePath(argv[i]);
		Mat image = matImageManager.Create(filePath);
		if (image.empty())
		{
			cout << "Couldn't load " << filePath << endl;
			continue;
		}

		Mat rect = numberPlateRectExtractor.Extract(image);

		std:string number = numberPlateStringExtractor.Extract(rect);
		cout << number << endl;
	}

	return 0;
}