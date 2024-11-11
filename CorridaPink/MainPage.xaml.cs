namespace CorridaPink;

public partial class MainPage : ContentPage
{
	bool EstaMorto = false;
	int LarguraJanela = 0;
	int AlturaJanela = 0;
	int Velocidade = 0;
	bool EstaPulando = false;
	const int TempoEntreFrames = 25;
	int velocidade1 = 0;
	int velocidade2 = 0;
	int velocidade3 = 0;
	int velocidade = 0;


	public MainPage()
	{
		InitializeComponent();
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalculaVelocidade(w);
	}

	void CalculaVelocidade(double w)
	{
		velocidade1 = (int)(w * 0.001);
		velocidade2 = (int)(w * 0.004);
		velocidade3 = (int)(w * 0.008);
		velocidade = (int)(w * 0.01);
	}

	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach (var a in HSLayer1.Children)
			(a as Image).WidthRequest = w;
		foreach (var a in HSLayer2.Children)
			(a as Image).WidthRequest = w;
		foreach (var a in HSLayer3.Children)
			(a as Image).WidthRequest = w;
		foreach (var a in HSLayerChao.Children)
			(a as Image).WidthRequest = w;

		HSLayer1.WidthRequest = w * 1.5;
		HSLayer2.WidthRequest = w * 1.5;
		HSLayer3.WidthRequest = w * 1.5;
		HSLayerChao.WidthRequest = w * 1.5;
	}

	void GerenciaCenarios()
	{
		MoveCenario();
		GerenciaCenario(HSLayer1);
		GerenciaCenario(HSLayer2);
		GerenciaCenario(HSLayer3);
		GerenciaCenario(HSLayerChao);
	}

	void MoveCenario()
	{
		HSLayer1.TranslationX -= velocidade1;
		HSLayer2.TranslationX -= velocidade2;
		HSLayer3.TranslationX -= velocidade3;
		HSLayerChao.TranslationX -= velocidade;
	}

	void GerenciaCenario(HorizontalStackLayout HSL)
	{
		var view = (HSL.Children.First() as Image);
		if (view.WidthRequest + HSL.TranslationX < 0)
		{
			HSL.Children.RemoveAt(view);
			HSL.Children.Add(view);
			HSL.TranslationX = view.TranslationX;
		}
	}

	async Task Desenha()
	{
		while (!EstaMorto)
		{
			GerenciaCenarios()
			await Task.Delay(TempoEntreFrames);
		}
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}

	public class Animação
	{
		protected List<String> animação1 = new List<String>();
		protected List<String> animação2 = new List<String>();
		protected List<String> animação3 = new List<String>();

		protected bool loop = true;
		protected int AnimacaoAtiva = 1;
		protected Image compImage;
		public Animação(Image a)
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
		public void SetAnimaçãoAtiva(int a)
		{
			SetAnimacaoAtiva = a;
		}
	}

	public void Desenha()
	{
		if (parado)
			return;
		String NomeArquivo = "01";
		int TamanhoAnimacao = 0;
		if (AnimacaoAtiva == 1)
		{
			NomeArquivo = animacao1[Frameatual];
			TamanhoAnimacao = animacao1.count;
		}
		else if (animacaoAtiva == 2)
		{
			NomeArquivo = animacao2[Frameatual];
			TamanhoAnimacao = animacao2.count;
		}
		else if (animacaoAtiva == 3)
		{
			NomeArquivo = animacao3[Frameatual];
			TamanhoAnimacao = animacao3.count;
		}
		CompImage.Source = ImageSource.FromFile(NomeArquivo);
		FrameAtual++;
		if (FrameAtual >= TamanhoAnimacao)
		{
			if (loop)
				FrameAtual = 0;
			else
			{
				Parado = true;
				QuandoParar();
			}
		}
	}
	public virtual void QuandoParar()
	{

	}

}

