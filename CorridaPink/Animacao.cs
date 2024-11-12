
using FFImageLoading.Maui;

public class Animação
{
    protected List<String> animacao1 = new List<String>();
    protected List<String> animacao2 = new List<String>();
    protected List<String> animacao3 = new List<String>();

    protected bool loop = true;
    bool parado = true;
    int FrameAtual=1;
    protected int AnimacaoAtiva = 1;
    protected CachedImageView compImage;
    public Animacao(CachedImageView a)
    {
        compImage = a;
    }

    public void Stop()
    {
        parado = true;
    }
    public void Play()
    {
        parado = false;
    }
    public void SetAnimacaoAtiva(int a)
    {
        SetAnimacaoAtiva=a;
    }

    public void Desenha()
    {
        if (parado)
            return;
        String NomeArquivo = "";
        int TamanhoAnimacao = 0;
        if (AnimacaoAtiva == 1)
        {
            NomeArquivo = animacao1[FrameAtual];
            TamanhoAnimacao = animacao1.Count;
        }
        else if (AnimacaoAtiva == 2)
        {
            NomeArquivo = animacao2[FrameAtual];
            TamanhoAnimacao = animacao2.Count;
        }
        else if (AnimacaoAtiva == 3)
        {
            NomeArquivo = animacao3[FrameAtual];
            TamanhoAnimacao = animacao3.Count;
        }
        compImage.Source = ImageSource.FromFile(NomeArquivo);
        FrameAtual++;
        if (FrameAtual >= TamanhoAnimacao)
        {
            if (loop)
                FrameAtual = 0;
            else
            {
                parado = true;
                QuandoParar();
            }
        }
    }
}

