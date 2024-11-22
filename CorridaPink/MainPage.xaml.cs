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
	const int ForcaGravidade = 6;
	bool EstanoChao = true;
	bool EstanoAr = false;
	int TempoPulando = 0;
	int TemponoAr = 0;
	const int ForcaPulo = 8;
	const int maxTempoPulando = 6;
	const int maxTemponoAr = 4;
	Player player;


	public MainPage()
	{
		InitializeComponent();
		player = new Player(menina);
		player.Run();
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

	void AplicaGravidade()
	{
		if (player.GetY() < 0)
			player.moveY(ForcaGravidade);
		else if (player.GetY() >= 0)
		{
			player.SetY(0);
			EstanoChao = true;
		}
	}

	async Task Desenha()
	{
		while (!EstaMorto)
		{
			GerenciaCenarios();
			Player Desenha();
			await Task.Delay(TempoEntreFrames);
			if (!EstaPulando && !EstanoAr)
			{
				AplicaGravidade();
				player.Desenha();
			}
			else
				AplicaPulo();
			await Task.Delay(TempoEntreFrames);
		}
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}

	void AplicaPulo()
	{
		EstanoChao = false;
		if (EstaPulando && TempoPulando >= maxTempoPulando)
		{
			EstaPulando = false;
			EstanoAr = true;
			TemponoAr = 0;
		}
		else if (EstanoAr && TemponoAr >= maxTemponoAr)
		{
			EstaPulando = false;
			EstanoAr = false;
			TempoPulando = 0;
			TemponoAr = 0;
		}
		else if (EstaPulando && TempoPulando < maxTemponoPulando)
		{
			player.MoveY(-ForcaPulo);
			TempoPulando++;
		}
		else if (EstanoAr)
			TemponoAr++;
	}

    void OnGridTapped(object o, TappedEventArgs a)
    {
        if (EstanoChao)
        EstaPulando=true;
    }

}

