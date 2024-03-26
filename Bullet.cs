using System;
using SplashKitSDK;

public class Bullet
{
    private double _X { get; set; }
    private double _Y { get; set; }
    public const int SPEED = 10;
    public const int Radius = 3;
    public Bullet(double X, double Y)
    {
        _X = X;
        _Y = Y;
    }
    public void Draw(Window window)
    {
        window.FillCircle(Color.Red, _X, _Y, Radius);
    }
    public void Update()
    {
        {
            _Y += -SPEED;
        }
    }
    public bool IsOffscreen(Window screen)
    {
        if (_Y < -Radius)
        {
            return true;
        }
        else if (_Y > screen.Height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public Circle CollideWith()
    {
        return SplashKit.CircleAt(_X, _Y, Radius);
    }
    public bool CollidedWith(Robot robot)
    {
        return SplashKit.CirclesIntersect(CollideWith(), robot.CollisionCircel);
    }

}
