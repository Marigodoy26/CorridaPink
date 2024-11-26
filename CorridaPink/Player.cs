using FFImageLoading.Maui;

namespace CorridaPink;
public delegate void Callback();

public class Player: Animação
{
    public Player(CachedImageView a):base(a)
    {
        for(int i=0; i<=15; ++i)
        animacao1.Add($"menina{i.ToString("D2")}.png");
       SetAnimacaoAtiva(1);
    }

    public void Die()
    {
        loop=false;
        SetAnimacaoAtiva(2);
    }

    public void Run()
    {
        loop=true;
        SetAnimacaoAtiva(1);
        Play();
    }

    public void MoveY(int s)
    {
        imageView.TranslationY+=s;
    }
    public double GetY()
    {
        return imageView.TranslationY;
    }
    public void SetY(double a )
    {
        imageView.TranslationY=a;
    }
}