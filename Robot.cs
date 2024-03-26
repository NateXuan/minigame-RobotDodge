using System;
using SplashKitSDK;

public abstract class Robot
{
    protected double X { get; set; }
    protected double Y { get; set; }
    protected Color MainColor;
    public Circle CollisionCircel { get; private set; }
    public Vector2D Velocity { get; set; }
    public bool ShouldBeRemoved { get; set; }

    public int Width
    {
        get { return 50; }
    }
    public int Height
    {
        get { return 50; }
    }

    public Robot(Window gameWindow, Player player)
    {
        //X = SplashKit.Rnd(gameWindow.Width - Width);
        //Y = SplashKit.Rnd(gameWindow.Height - Height);
        MainColor = Color.RandomRGB(200);

        if (SplashKit.Rnd() < 0.5)
        {
            X = SplashKit.Rnd(gameWindow.Width);
            if (SplashKit.Rnd() < 0.5) Y = -Height;
            else Y = gameWindow.Height;
        }
        else
        {
            Y = SplashKit.Rnd(gameWindow.Height);
            if (SplashKit.Rnd() >= 0.5) X = gameWindow.Width;
            else X = -Width;
        }

        const int SPEED = 4;
        Point2D fromPT = new Point2D() { X = X, Y = Y };
        Point2D toPT = new Point2D() { X = player.X, Y = player.Y };

        //Caculate the direction to head.
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPT, toPT));
        Velocity = SplashKit.VectorMultiply(dir, SPEED);

        CollisionCircel = SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 20);
    }

    public abstract void Draw(Window window);

    public void Update()
    {
        X = X + Velocity.X;
        Y = Y + Velocity.Y;
        CollisionCircel = SplashKit.CircleAt(X + Velocity.X + Width / 2, Y + Velocity.Y + Height / 2, 20);
    }

    public bool IsOffscreen(Window screen)
    {
        if (X < -Width)
        {
            return true;
        }
        else if (X > screen.Width)
        {
            return true;
        }
        else if (Y < -Height)
        {
            return true;
        }
        else if (Y > screen.Height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class Boxy : Robot
{
    public Boxy(Window gameWindow, Player player) : base(gameWindow, player)
    {
        
    }

    public override void Draw(Window window)
    {
        double leftX;
        double rightX;
        double eyeY;
        double mouthY;
        leftX = X + 12;
        rightX = X + 27;
        eyeY = Y + 10;
        mouthY = Y + 30;
        window.FillRectangle(Color.LightGray, X, Y, 50, 50);
        window.FillRectangle(MainColor, leftX, eyeY, 10, 10);
        window.FillRectangle(MainColor, rightX, eyeY, 10, 10);
        window.FillRectangle(MainColor, leftX, mouthY, 25, 10);
        window.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
    }
}

public class Roundy : Robot
{
    public Roundy(Window gameWindow, Player player) : base(gameWindow, player)
    {

    }
    public override void Draw(Window window)
    {
        double leftX;
        double midX;
        double rightX;
        double midY;
        double eyeY;
        double mouthY;
        leftX = X + 17;
        midX = X + 25;
        rightX = X + 33;
        midY = Y + 25;
        eyeY = Y + 20;
        mouthY = Y + 35;
        window.FillCircle(Color.White, midX, midY, 25);
        window.DrawCircle(Color.Gray, midX, midY, 25);
        window.FillCircle(MainColor, leftX, eyeY, 5);
        window.FillCircle(MainColor, rightX, eyeY, 5);
        window.FillEllipse(Color.Gray, X, eyeY, 50, 30);
        window.DrawLine(Color.Black, X, mouthY, X + 50, Y + 35);
    }
}

public class Triangle : Robot
{
    public Triangle(Window gameWindow, Player player) : base(gameWindow, player)
    {

    }
    public override void Draw(Window window)
    {
        double leftX;
        double rightX;
        double eyeY;
        double mouthY;
        leftX = X + 12;
        rightX = X + 27;
        eyeY = Y + 10;
        mouthY = Y + 30;
        window.FillTriangle(Color.LightGray, X - 10, Y + 50, X + 25, Y - 25, X + 60, Y + 50);
        window.FillRectangle(MainColor, leftX, eyeY, 10, 10);
        window.FillRectangle(MainColor, rightX, eyeY, 10, 10);
        window.FillRectangle(MainColor, leftX, mouthY, 25, 10);
    }
}