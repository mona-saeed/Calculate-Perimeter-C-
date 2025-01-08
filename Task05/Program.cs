class Program
{
   enum Shape { Square, Rectangle, Triangle }
    static bool IsValidShape(Shape shape) => Enum.IsDefined(typeof(Shape), shape);
    static bool AreValidShapeDimensions(Shape shape, params double[] dimensions)
    {
        //In case AreValidShapeDimensions(Shape.Square) params makes the parameter optional
        //evaluates to an empty array if omitted or checks for null explicitly
        if (dimensions == null || dimensions.Length == 0)
        {
            Console.WriteLine("No dimensions provided.");
            return false;
        }
        if (!IsValidShape(shape))
        {
            Console.WriteLine("Invalid shape specified.");
            return false;
        }
        int expectedDimensions = shape switch
        {
            Shape.Square => 1,
            Shape.Rectangle => 2,
            Shape.Triangle => 3,
            _ => 0 
        };
        if (dimensions.Length != expectedDimensions)
        {
            Console.WriteLine($"Expected {expectedDimensions} dimensions for {shape}, but received {dimensions.Length}.");
            return false;
        }
        //Can use LINQ Any() for brevity, dimensions.Any(dimension => dimension <= 0) instead of foreach
        foreach (var dimension in dimensions)
        {
            if (dimension <= 0)
            {
                Console.WriteLine(GetErrorMessage(shape));
                return false;
            }
        }
        return true;
    }
    static bool AreValidTriangleDimensions(double triangleSideOne, double triangleSideTwo, double triangleSideThree)
    {
        if (triangleSideOne + triangleSideTwo <= triangleSideThree || triangleSideOne + triangleSideThree <= triangleSideTwo
        || triangleSideTwo + triangleSideThree <= triangleSideOne)
        {
            Console.WriteLine("Invalid triangle dimensions (does not satisfy triangle inequality).");
            return false;
        }
        return true;
    }
    static bool AreValidRectangleDimensions(double rectangleLength, double rectangleWidth)
    {
        if (rectangleLength == rectangleWidth)
        {
            Console.WriteLine("Length and width are equal, which is a square, not a rectangle.");
            return false;
        }
        return true;
    }
    static string GetErrorMessage(Shape shape)
    {
        return shape switch
        {
            Shape.Square => "Invalid side length for square.",
            Shape.Rectangle => "Invalid rectangle dimensions.",
            Shape.Triangle => "Invalid side length for triangle.",
            _ => "Unknown shape"
        };
    }
    static double GetSquarePerimeter(double sideLength) => sideLength * 4.0;
    static double GetRectanglePerimeter(double rectangleLength, double rectangleWidth) =>
        (rectangleLength + rectangleWidth) * 2.0;
    static double GetTrianglePerimeter(double triangleSideOne, double triangleSideTwo, double triangleSideThree) =>
        triangleSideOne + triangleSideTwo + triangleSideThree;
    static void FormatPerimeterShape(Shape shape, double perimeterShape) =>
        Console.WriteLine($"Shape = {shape,-12}Perimeter = {perimeterShape}");
    static void CalculatePerimeter(double sideLength)
    {
        if (AreValidShapeDimensions(Shape.Square, sideLength))
        {
            FormatPerimeterShape(Shape.Square, GetSquarePerimeter(sideLength));
        }
    }
    static void CalculatePerimeter(double rectangleLength, double rectangleWidth)
    {
        if (AreValidShapeDimensions(Shape.Rectangle, rectangleLength, rectangleWidth) &&
            AreValidRectangleDimensions(rectangleLength, rectangleWidth))
        {
            FormatPerimeterShape(Shape.Rectangle, GetRectanglePerimeter(rectangleLength, rectangleWidth));
        }
    }
    static void CalculatePerimeter(double triangleSideOne, double triangleSideTwo, double triangleSideThree)
    {
        if (AreValidShapeDimensions(Shape.Triangle, triangleSideOne, triangleSideTwo, triangleSideThree) &&
            AreValidTriangleDimensions(triangleSideOne, triangleSideTwo, triangleSideThree))
        {
            FormatPerimeterShape(Shape.Triangle, GetTrianglePerimeter(triangleSideOne, triangleSideTwo, triangleSideThree));
        }
    }
    static void Main()
    {
        CalculatePerimeter(5);
        CalculatePerimeter(5, 10);
        CalculatePerimeter(3, 4, 5);
    }
}
