using System;
using System.Globalization;
using System.Collections.Generic;

namespace AdventureGame
{
    class Program
    {
        // Variáveis principais
        static int opcaoEscolhida;
        static int pontosVida;
        static int ataque;
        static int pontosMagia;
        static int ouro;
        static int qtdComprada;
        static int qtdUsada;
        static int turnos;

        // Variáveis de inicialização - Jogador
        static int pontosVidaMaximo = 500;
        static int pontosMagiaMaximo = 100;
        static int ataqueInicial = 100;
        static int ouroInicial = 300;
        static int qtdPocaoInicial = 0;
        static int qtdElixirInicial = 0;
        static int magiaDispararEnergia = 50;

        // Dados dos itens (Poção - Elixir)
        static string[] itens = { "Poção", "Elixir" };
        static int[] qtdItens = { 0, 0 };
        static int[] precoItens = { 100, 100 };
        static int[] recuperacaoItens = { 500, 50 };

        // Variáveis de inicialização - MONSTRO NORMAL
        static int pontosVidaMonstro = 500;
        static int ataqueMonstro = 100;
        static int[] recompensaOuroMonstro = { 50, 100 };

        // Variáveis de inicialização - CHEFE
        static int pontosVidaChefe = 5000;
        static int ataqueChefe = 250;
        static int[] recompensaOuroChefe = { 500, 1000 };

        // Variáveis do monstro
        static int pontosVidaAdversarioMaximo;
        static int pontosVidaAdversario;
        static int ataqueAdversario;
        static int[] recompensaOuroAdversario = { 0, 0 };

        // Recompensa partida
        static int recompensaVidaVitoria = 10;
        static int recompensaRecebida;
        static Random r = new Random();

        static string[] acoesMenuInicial =
        {
            "Visitar loja",
            "Dormir",
            "Explorar Masmorra",
            "Sair do jogo"
        };

        static string[] acoesVisitarLoja =
        {
            "Poção - Recupera 500 pontos de vida - Preço 100 moedas de ouro",
            "Elixir - Recupera 50 pontos de magia - Preço 100 moedas de ouro"
        };

        static string[] acoesMasmorra =
{
            "Entrar em uma sala de monstro",
            "Entrar na sala do chefe",
            "Voltar ao Menu Inicial",
        };

        static string[] menuCombate =
        {
            "Atacar",
            "Disparar Energia",
            "Usar item"
        };

        static void Main(string[] args)
        {
            bool continuarJogando = true;

            // Inicialização
            init();

            while (continuarJogando)
            {
                // Menu Inicial
                VisitarMenuInicial();

                // Visitar Loja
                if (Program.opcaoEscolhida == 1)
                {
                    VisitarLoja();
                }

                // Dormir
                else if (Program.opcaoEscolhida == 2)
                {
                    Dormir();
                }

                // Explorar Masmorra
                else if (Program.opcaoEscolhida == 3)
                {
                    ExplorarMasmorra();

                    if (Program.opcaoEscolhida == 1)
                    {
                        initMonstroNormal();
                        SistemaDeBatalha();
                    }

                    else if (Program.opcaoEscolhida == 2)
                    {
                        initMonstroChefe();
                        SistemaDeBatalha();
                    }

                    else if (Program.opcaoEscolhida == 3)
                    {
                        // Voltar ao Menu Inicial
                        continue;
                    }

                }

                // Sair do jogo
                else if (Program.opcaoEscolhida == 4)
                {
                    Console.WriteLine("Obrigado por jogar o Adventure Game!");
                    continuarJogando = false;
                }

            }

        }

        static void init()
        {
            Program.pontosVida = Program.pontosVidaMaximo;
            Program.pontosMagia = Program.pontosMagiaMaximo;
            Program.ataque = Program.ataqueInicial;
            Program.ouro = Program.ouroInicial;
            Program.qtdItens[0] = Program.qtdPocaoInicial;
            Program.qtdItens[1] = Program.qtdElixirInicial;
        }

        static void ListarAcoes(string[] menuAcoes)
        {
            for (int i = 0; i < menuAcoes.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {menuAcoes[i]}");
            }
        }
        static void ListarItensDisponiveis()
        {
            Console.WriteLine($"Total de moedas: {Program.ouro}");
            for (int i = 0; i < Program.itens.Length; i++)
            {
                Console.WriteLine($"{Program.itens[i]}: {Program.qtdItens[i]}");
            }
        }

        static void ValidarInputMenu(string[] menuAcoes, bool inputValido = false)
        {
            int inputUsuario;

            while (!inputValido)
            {
                inputValido = (int.TryParse(Console.ReadLine(), out inputUsuario)
                                && inputUsuario > 0
                                && inputUsuario <= menuAcoes.Length);

                if (inputValido)
                {
                    // Atualizando o valor do menu inicial
                    AtualizarOpcaoEscolhida(inputUsuario);

                    break;
                }

                Console.Write("Tente novamente! Por favor, digite um número válido: ");
            }

        }
        static void ValidarInputQuantidade(bool inputValido = false)
        {
            int outputUsuario;

            while (!inputValido)
            {
                inputValido = int.TryParse(Console.ReadLine(), out outputUsuario);

                if (inputValido)
                {
                    Program.qtdComprada = outputUsuario;
                    break;
                }

                Console.Write("Por favor, digite uma quantidade válida: ");
            }
        }

        static int AtualizarOpcaoEscolhida(int inputUsuario)
        {
            return Program.opcaoEscolhida = inputUsuario;
        }

        static void AtualizarBolsaCompra()
        {
            Program.qtdItens[Program.opcaoEscolhida - 1] = Program.qtdItens[Program.opcaoEscolhida - 1] + Program.qtdComprada;
        }

        static void AtualizarOuro()
        {
            int ouroUtilizado = Program.precoItens[Program.opcaoEscolhida - 1] * Program.qtdComprada;
            Program.ouro = Program.ouro - ouroUtilizado;
        }

        static void VisitarMenuInicial()
        {
            Console.WriteLine("----- MENU INICIAL -----");
            ListarAcoes(Program.acoesMenuInicial);

            Console.WriteLine();
            Console.Write("Digite uma das ações: ");
            ValidarInputMenu(Program.acoesMenuInicial);

            Console.WriteLine();
        }

        static void VisitarLoja()
        {

            Console.WriteLine("----- LOJA -----");
            Console.WriteLine("Olá estranho! O que você está comprando?");
            Console.WriteLine($"Total de Moedas: {Program.ouro}");

            Console.WriteLine();
            ListarAcoes(Program.acoesVisitarLoja);

            Console.WriteLine();
            Console.Write("Digite uma das ações: ");
            ValidarInputMenu(Program.acoesVisitarLoja);

            Console.Write("Digite a quantidade que você quer comprar: ");
            ValidarInputQuantidade();

            if (Program.precoItens[Program.opcaoEscolhida - 1] * Program.qtdComprada > Program.ouro)
            {
                Console.WriteLine();
                Console.WriteLine("Quantidade de moedas insuficiente!");
                Console.WriteLine();
                return;
            }

            else
            {
                AtualizarOuro();
                AtualizarBolsaCompra();
            }

            Console.WriteLine();
            Console.WriteLine("------ SEUS ITENS ------");
            ListarItensDisponiveis();
            Console.WriteLine("------------------------");
            Console.WriteLine();
        }

        static void ExplorarMasmorra()
        {
            Console.WriteLine("----- MASMORRA -----");
            ListarAcoes(Program.acoesMasmorra);

            Console.WriteLine();
            Console.Write("Digite uma das ações: ");
            ValidarInputMenu(Program.acoesMasmorra);

            Console.WriteLine();
        }

        static void Dormir()
        {
            Console.WriteLine("Recepcionista: Bem-vindo a nossa taverna aventureiro. Tenha ótimos sonhos");
            Console.WriteLine("DORMINDO...");

            Program.pontosVida = Program.pontosVidaMaximo;
            Program.pontosMagia = Program.pontosMagiaMaximo;

            System.Threading.Thread.Sleep(2000);

            Program.ResumoStatusJogador();

            Console.WriteLine();
            Console.WriteLine("Recepcionista: Boas Aventuras!");
            Console.WriteLine();
        }

        static void SistemaDeBatalha()
        {
            Program.turnos = 1;

            Console.WriteLine("--------------------------");
            Console.WriteLine("Você encontrou um inimigo!");
            Console.WriteLine();
            Console.WriteLine($"----- MONSTRO -----");
            Console.WriteLine($"-- Pontos de Vida: {Program.pontosVidaAdversario}/{Program.pontosVidaAdversarioMaximo}");
            Console.WriteLine();

            // Enquanto um dos dois estiverem vivos
            while (Program.pontosVida > 0 && Program.pontosVidaAdversario > 0)
            {
                Console.WriteLine("----- MENU DE COMBATE -----");
                ListarAcoes(menuCombate);

                Console.WriteLine();
                Console.Write("Digite uma das ações: ");
                ValidarInputMenu(Program.menuCombate);

                // Atacar
                if (Program.opcaoEscolhida == 1)
                {
                    Atacar();
                }

                // Disparar Energia
                else if (Program.opcaoEscolhida == 2)
                {
                    if (Program.magiaDispararEnergia > Program.pontosMagia)
                    {
                        Console.WriteLine();
                        Console.WriteLine("-- Pontos de magia insuficiente!");
                        Console.WriteLine();
                        continue;
                    }
                    DispararEnergia();
                }

                // Usar item
                else if (Program.opcaoEscolhida == 3)
                {
                    if (Program.qtdItens[0] == 0 && Program.qtdItens[1] == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("-- Nenhum item disponível!");
                        Console.WriteLine();
                        continue;
                    }

                    EscolherItem();

                    if (Program.qtdItens[Program.opcaoEscolhida - 1] == 0)
                    {
                        Console.WriteLine($"-- Quantidade disponível: 0");
                        Console.WriteLine("-- Tente outra ação!");
                        continue;
                    }

                    if (Program.opcaoEscolhida == 1)
                    {
                        AtualizarVida(Program.opcaoEscolhida);
                    }
                    else
                    {
                        AtualizarMagia(Program.opcaoEscolhida);
                    }
                    AtualizarQtdItem();
                }

                // Verificando se a batalha terminou após o turno do jogador
                if (Program.pontosVidaAdversario <= 0)
                {
                    JogadorVence();
                    return;
                }

                // TURNO DO MONSTRO
                AtaqueMonstro();
                Console.WriteLine();

                // Verificando se a batalha terminou após o turno do monstro
                if (Program.pontosVida <= 0)
                {
                    JogadorPerde();
                    return;
                }

                // RESOLUÇÃO DO TURNO
                ResolucaoTurno();

                // Atualização dos turnos
                Program.turnos++;

            }
        }

        // Inicialização dos monstros
        static void initMonstroNormal()
        {
            Program.pontosVidaAdversarioMaximo = Program.pontosVidaMonstro;
            Program.pontosVidaAdversario = Program.pontosVidaMonstro;
            Program.ataqueAdversario = Program.ataqueMonstro;
            Program.recompensaOuroAdversario[0] = Program.recompensaOuroMonstro[0];
            Program.recompensaOuroAdversario[1] = Program.recompensaOuroMonstro[1];
        }
        static void initMonstroChefe()
        {
            Program.pontosVidaAdversarioMaximo = Program.pontosVidaChefe;
            Program.pontosVidaAdversario = Program.pontosVidaChefe;
            Program.ataqueAdversario = Program.ataqueChefe;
            Program.recompensaOuroAdversario[0] = Program.recompensaOuroChefe[0];
            Program.recompensaOuroAdversario[1] = Program.recompensaOuroChefe[1];
        }

        // Ações do JOGADOR
        static void Atacar()
        {
            Program.pontosVidaAdversario = Program.pontosVidaAdversario - Program.ataque;
        }

        static void DispararEnergia()
        {

            Program.pontosMagia = Program.pontosMagia - Program.magiaDispararEnergia;

            Program.pontosVidaAdversario = Program.pontosVidaAdversario - (2 * Program.ataque);

        }

        static void EscolherItem()
        {
            // Listando itens
            Console.WriteLine("----- MENU ITENS -----");
            ListarAcoes(Program.itens);

            Console.WriteLine();
            Console.Write("Digite um dos itens: ");
            ValidarInputMenu(Program.itens);
        }

        static void AtualizarVida(int indexItemUsado)
        {
            Program.pontosVida += Program.recuperacaoItens[indexItemUsado - 1];

            if (Program.pontosVida > Program.pontosVidaMaximo)
            {
                Program.pontosVida = Program.pontosVidaMaximo;
            }
        }

        static void AtualizarMagia(int indexItemUsado)
        {
            Program.pontosMagia += Program.recuperacaoItens[indexItemUsado - 1];

            if (Program.pontosMagia > Program.pontosMagiaMaximo)
            {
                Program.pontosMagia = Program.pontosMagiaMaximo;
            }
        }

        static void AtualizarQtdItem()
        {
            Program.qtdItens[Program.opcaoEscolhida - 1] = Program.qtdItens[Program.opcaoEscolhida - 1] - 1;
        }

        static void JogadorVence()
        {
            Console.WriteLine($"Parabéns jogador! Você derrotou o monstro em {Program.turnos} turnos.");

            // Recompensa do jogador
            Program.pontosVidaMaximo += Program.recompensaVidaVitoria;
            Program.recompensaRecebida = Program.r.Next(recompensaOuroAdversario[0], recompensaOuroAdversario[1]);
            Program.ouro += Program.recompensaRecebida;

            Console.WriteLine($"Vida máxima aumentada para {Program.pontosVidaMaximo}");
            Console.WriteLine($"{Program.recompensaRecebida} moedas de ouro recebidas");

            Console.WriteLine();
            Console.WriteLine("Retornando a sala anterior");

            // Ir para a Masmorra
            Program.opcaoEscolhida = 3;
        }

        static void JogadorPerde()
        {
            Console.WriteLine("Monstro: HAHAHA! Fraco, muito fraco! Mandei mais aventureiros para me entreter mais");
            Console.WriteLine();
            Console.WriteLine("-- O jogador foi derrotado e foi encaminhado para a taverna na cidade");
            Console.WriteLine();

            // Ir para Dormir()
            Program.opcaoEscolhida = 2;
        }

        static void ResolucaoTurno()
        {
            Console.WriteLine();
            Console.WriteLine("----- JOGADOR -----");
            Console.WriteLine($"-- Pontos de Vida: {Program.pontosVida}/{Program.pontosVidaMaximo}");
            Console.WriteLine($"-- Pontos de Magia: {Program.pontosMagia}/{Program.pontosMagiaMaximo}");
            Console.WriteLine();

            Console.WriteLine("----- MONSTRO -----");
            Console.WriteLine($"-- Pontos de Vida: {Program.pontosVidaAdversario}/{Program.pontosVidaAdversarioMaximo}");
            Console.WriteLine();
        }

        static void ResumoStatusJogador()
        {
            Console.WriteLine();
            Console.WriteLine("----- JOGADOR -----");
            Console.WriteLine($"-- Pontos de Vida: {Program.pontosVida}/{Program.pontosVidaMaximo}");
            Console.WriteLine($"-- Pontos de Magia: {Program.pontosMagia}/{Program.pontosMagiaMaximo}");
        }

        // Ações do Mosntro
        static void AtaqueMonstro()
        {
            Program.pontosVida = Program.pontosVida - Program.ataqueAdversario;
        }



    }
}