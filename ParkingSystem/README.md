# Recognition of license plates in the picture

The project is responsible for finding license plates in the pictures from the parking lot

1. The image is converted to shades of gray
2. By thresholding, we make a stamp from the photo (threshold). Then we can find and mark contours and detect geometrical figures in the picture.
3. Then, search for license plate contours and their verification:
- remove the contours located at the image edge
- search for contours with 4 vertices
- check if the angles between the straight lines are within the given range
- check the straight lines proportions
4. We create a mask based on the received vertices and cut the license plate from the picture. We get a vector describing the license plate
5. We rotate the license plate to get the shape of a rectangle
6. We cut 20 pixels from the left to remove the blue bar from license plate
7. Using a separate application, we created a model table for the neural network learning
