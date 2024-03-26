using System;
using SplashKitSDK;

public class RobotDodge
{
    private Player _Player;
    private Window _GameWindow;
    private List<Robot> _Robots;
    private List<Robot> _RemoveRobots;
    private List<Bullet> _bullets = new List<Bullet>();
    public bool Quit
    {
        get
        {
            return _Player.Quit;
        }
    }
    public RobotDodge(Window window, Player player)
    {
        _GameWindow = window;
        _Player = player;
        _Robots = new List<Robot>();
        _RemoveRobots = new List<Robot>();
        //_TestRobot = RandomRobot(_GameWindow);
    }

    public void HandleInput(Window window)
    {
        _Player.HandleInput(_GameWindow);
    }

    public void StayOnWindow(Window window)
    {
        _Player.StayOnWindow(_GameWindow);

    }

    public void Draw(Window window)
    {
        _GameWindow.Clear(Color.White);
        //_TestRobot.Draw(window);
        foreach (Robot robot in _Robots)
        {
            robot.Draw(_GameWindow);
        }
        _Player.Draw(_GameWindow);
        _Player.DrawLives();
        _Player.DrawScore();
        _Player.Update();
        _GameWindow.Refresh(60);
    }
    public void Update()
    {
        // _TestRobot.Update();

        // if (_Player.CollideWith(_TestRobot) == true)
        // {
        //     _TestRobot = RandomRobot(_GameWindow);
        // }
        // else if (_TestRobot.IsOffscreen(_GameWindow) == true)
        // {
        //     _TestRobot = RandomRobot(_GameWindow);
        // }
        foreach (Robot robot in _Robots)
        {
            robot.Update();
        }
        Random random = new Random();
        if (random.NextDouble() < 0.02)
        {
            _Robots.Add(RandomRobot(_GameWindow));
        }
        CheckCollisions();
        _Player.Update();
    }

    public Robot RandomRobot(Window window)
    {
        Random random = new Random();
        if (random.NextDouble() <= 0.33)
        {
            Boxy boxy = new Boxy(_GameWindow, _Player);
            return boxy;
        }
        else if (random.NextDouble() > 0.33 && random.NextDouble() <= 0.66)
        {
            Roundy roundy = new Roundy(_GameWindow, _Player);
            return roundy;
        }
        else
        {
            Triangle triangle = new Triangle(_GameWindow, _Player);
            return triangle;
        }
    }

    public void CheckCollisions()
    {
        foreach (Robot robot in _Robots)
        {
            _Player.CheckCollisions(robot, _GameWindow);
            if (robot.ShouldBeRemoved)
            {
            _RemoveRobots.Add(robot);
            }
            else if (_Player.CollideWith(robot) == true)
            {
                _Player._lives--;
                if (_Player._lives <= 0)
                {
                    _Player.Quit = true;
                    GameOver(_GameWindow);
                    break;
                }
                _RemoveRobots.Add(robot);
            }
            else if (robot.IsOffscreen(_GameWindow) == true)
            {
                _RemoveRobots.Add(robot);
            }
            // foreach (Bullet bullet in _bullets)
            // {
            //     if (bullet.CollidedWith(robot))
            //     {
            //         _RemoveRobots.Add(robot);
            //         _bullets.Remove(bullet);
            //         break;
            //     }
            // }
        }
        foreach (Robot robot in _RemoveRobots)
        {
            _Robots.Remove(robot);
        }
        _RemoveRobots.Clear();
    }
    public void GameOver(Window window)
    {
        string gameOverMessage = "Game Over";
        float textX = window.Width / 2 - SplashKit.TextWidth(gameOverMessage, "Arial.ttf", 20) / 2;
        float textY = window.Height / 2 - SplashKit.TextHeight(gameOverMessage, "Arial.ttf", 20) / 2;
        window.DrawText(gameOverMessage, Color.Red, "Arial.ttf", 35, textX, textY);
    }
}
