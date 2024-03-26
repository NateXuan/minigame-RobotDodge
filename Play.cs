using System;
using SplashKitSDK;

public class Player
{
    public Bitmap _PlayerBitmap;
    public double X { get; private set; }
    public double Y { get; private set; }
    public bool Quit { get; set; }
    private List<Bullet> _bullets = new List<Bullet>();
    private List<Bullet> _RemoveBullets = new List<Bullet>();
    public int _lives;
    private int _score = 0;
    private SplashKitSDK.Timer _gameTimer = new SplashKitSDK.Timer("GameTime");
    private SplashKitSDK.Timer _shootTimer;
    private double ShootInterval { get; set; }
    public int Width
    {
        get
        {
            return this._PlayerBitmap.Width;
        }
    }

    public int Height
    {
        get
        {
            return this._PlayerBitmap.Height;
        }
    }

    public Player(Window gameWindow)
    {
        _PlayerBitmap = new Bitmap("Player", "Player.png");
        X = (gameWindow.Width - this.Width) / 2;
        Y = (gameWindow.Height - this.Height) / 2;
        Quit = false;
        _gameTimer.Start();
        _lives = 5;
        _shootTimer = new SplashKitSDK.Timer("Shoot Timer");
        _shootTimer.Start();
        ShootInterval = 300;
    }

    public void Draw(Window window)
    {
        window.DrawBitmap(_PlayerBitmap, X, Y);
        foreach (Bullet bullet in _bullets)
        {
            bullet.Draw(window);
        }
    }

    public void HandleInput(Window window)
    {
        const int SPEED = 5;
        if (SplashKit.KeyDown(KeyCode.EscapeKey))
        {
            window.Close();
        }
        else if (SplashKit.KeyDown(KeyCode.RightKey))
        {
            X += SPEED;
        }
        else if (SplashKit.KeyDown(KeyCode.LeftKey))
        {
            X -= SPEED;
        }
        else if (SplashKit.KeyDown(KeyCode.UpKey))
        {
            Y -= SPEED;
        }
        else if (SplashKit.KeyDown(KeyCode.DownKey))
        {
            Y += SPEED;
        }
        else if (SplashKit.KeyDown(KeyCode.SpaceKey))
        {
            if (_shootTimer.Ticks >= ShootInterval)
            {
                shoot();
                _shootTimer.Reset();
            }
        }
        else
        {
            X = X;
            Y = Y;
        }
    }


    public void StayOnWindow(Window window)
    {
        const int GAP = 10;
        if (X < GAP)
        {
            X = GAP;
        }
        else if ((X + Width) > (window.Width - GAP))
        {
            X = window.Width - GAP - Width;
        }
        else
        {
            X = X;
        }
        if (Y < GAP)
        {
            Y = GAP;
        }
        else if ((Y + Height) > (window.Height - GAP))
        {
            Y = window.Height - GAP - Height;
        }
        else
        {
            Y = Y;
        }
    }

    public bool CollideWith(Robot other)
    {
        return _PlayerBitmap.CircleCollision(X, Y, other.CollisionCircel);
    }
    public void shoot()
    {
        double bulletX = X + Width / 2;
        double bulletY = Y - Bullet.Radius - Bullet.SPEED;
        _bullets.Add(new Bullet(bulletX, bulletY));
    }
    public void DrawLives()
    {
        SplashKit.LoadFont("Arial", "arial.ttf");
        SplashKit.DrawText($"Lives:{_lives}", Color.Black, "Arial", 20, 900, 20);
    }

    public void DrawScore()
    {
        SplashKit.LoadFont("Arial", "arial.ttf");
        SplashKit.DrawText($"Score:{_score}", Color.Black, "Arial", 20, 800, 20);
    }
    public void Update()
    {
        if (_gameTimer.Ticks >= 1000)
        {
            _score++;
            _gameTimer.Reset();
        }
        foreach (Bullet bullet in _bullets)
        {
            bullet.Update();
        }
        _bullets.RemoveAll(bullet => bullet.IsOffscreen(SplashKit.WindowNamed("Shape Dodge")));
    }
    public void CheckCollisions(Robot robot, Window window)
    {
        foreach (Bullet bullet in _bullets)
        {
            _RemoveBullets.Clear();
            if (bullet.CollidedWith(robot))
            {
                _RemoveBullets.Add(bullet);
                robot.ShouldBeRemoved = true;
            }
            else if (bullet.IsOffscreen(window))
            {
                _RemoveBullets.Add(bullet);
            }
        }
        foreach (Bullet bullet in _RemoveBullets)
        {
            _bullets.Remove(bullet);
        }
        _RemoveBullets.Clear();
    }
}
