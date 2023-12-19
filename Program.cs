// See https://aka.ms/new-console-template for more information
// паттерн Адаптер
// структурный паттерн
// позволяет использовать несовместимые объекты по
// заданному интерфейсу
// Адаптер может переадресовывать вызовы из метода по
// интерфейсу к хранящемуся в нем объекту или преобразовывать
// данные к виду, с которым работает хранящийся объект

using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;

DrawOnConsole drawOnConsole = new DrawOnConsole();
var adapter = new AdapterCar(new Car());
adapter.Point = new Point(100, 100);
drawOnConsole.DrawObject(adapter);
drawOnConsole.DrawObject(new Circle()); // объект реализует интерфейс, к нему адаптер не нужен
drawOnConsole.DrawObject(new AdapterHuman(new Human()));
Console.ReadLine();

class DrawOnConsole
{
    Graphics graphics;
    public DrawOnConsole()
    {
        graphics = Graphics.FromHwndInternal(Process.GetCurrentProcess().MainWindowHandle);
    }
    
    public void DrawObject(IDraw draw)
    {
        draw.Draw(graphics);
    }
}
// интерфейс для рисования объектов, каждый объект должен знать
// как себя нарисовать
interface IDraw
{
    void Draw(Graphics graphics);
}

public class Circle : IDraw
{
    public void Draw(Graphics graphics)
    {
        graphics.FillEllipse(Brushes.White, 200, 100, 50, 50);
    }
}
// адаптер для машины позволяет её нарисовать
public class AdapterCar : IDraw
{
    private readonly Car car;

    public Point Point { get; set; }

    public AdapterCar(Car car)
    {
        this.car = car;
    }

    public void Draw(Graphics graphics)
    {
        car.Draw(graphics, Point.X, Point.Y);
    }
}
// адаптер для человека позволяет его нарисовать
public class AdapterHuman : IDraw
{
    private readonly Human human;

    public Point Point { get; set; }

    public AdapterHuman(Human human)
    {
        this.human = human;
    }

    public void Draw(Graphics graphics)
    {
        human.Draw(graphics, Point.X, Point.Y);
    }
}
// представим, что классы дальше находятся в чужих библиотеках
// и мы не можем их менять
public class Car
{
    public void Draw(Graphics graphics, int x, int y)
    {
        graphics.DrawRectangle(Pens.Red, x, y, 200, 50);
    }
}

public class Human
{
    public void Draw(Graphics graphics, int x, int y)
    {
        graphics.DrawRectangle(Pens.Green, x, y, 50, 200);
    }
}