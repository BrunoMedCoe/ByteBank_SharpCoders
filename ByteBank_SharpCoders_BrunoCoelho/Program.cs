using System;

namespace ByteBank_SharpCoders_BrunoCoelho
{
    public class Program
    {

        static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Manipular a conta");
            Console.WriteLine("0 - Para sair do programa");
            Console.WriteLine("---------------------------------------");
            Console.Write("\nDigite a opção desejada: ");
        }

        public static string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            Console.WriteLine();
            return pass;
        }

        static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("\nDigite o cpf: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o nome: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite a senha: ");
            senhas.Add(GetPassword());
            saldos.Add(0);
        }

        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("\nDigite o cpf: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1)
            {
                Console.WriteLine("Não foi possível deletar esta conta");
                Console.WriteLine("Motivo: Conta não encontrada");
            }

            cpfs.Remove(cpfParaDeletar);
            titulares.RemoveAt(indexParaDeletar);
            senhas.RemoveAt(indexParaDeletar);
            saldos.RemoveAt(indexParaDeletar);

            Console.WriteLine("Conta deletada com sucesso.");
        }

        static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            for (int i = 0; i < cpfs.Count; i++)
            {
                ApresentaConta(i, cpfs, titulares, saldos);
            }
        }

        static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Write("\nDigite o cpf: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaApresentar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaApresentar== -1)
            {
                Console.WriteLine("Não foi possivel presentar esta Conta.");
                Console.WriteLine("Motivo: Conta não encontrada.");
            }

            ApresentaConta(indexParaApresentar, cpfs, titulares, saldos);
        }

        static void ApresentarValorAcumulado(List<double> saldos)
        {
            Console.WriteLine($"Total acumulado no banco: R$ {saldos.Sum()}");
        }

        static void ApresentaConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo: R$ {saldos[index]:f2}");
        }

        static void QuantiaArmazenada(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine();
            ApresentarValorAcumulado(saldos);
            Console.WriteLine("Pressione Enter para continuar.");
            Console.ReadKey();
        }

        static void ManipularConta(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {        
            Console.WriteLine();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("[1] - Realizar um depósito");
            Console.WriteLine("[2] - Realizar um saque");
            Console.WriteLine("[3] - Realizar uma transferência");
            Console.WriteLine("[9] - Voltar para o menu anterior.");
            Console.WriteLine("---------------------------------------");
            Console.Write("\nDigite a opção desejada: ");
        }

        static void RealizarSaque(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("\nDigite o cpf: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaConsultar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaConsultar == -1)
            {
                Console.WriteLine("Não foi possível encontrar esta conta");
                Console.WriteLine("Confira o CPF e tente novamente.");
            }
            else
            {
                Console.Write("\nDigite sua senha: ");
                string senhaParaApresentar = GetPassword();
                int indexSenhaConsultar = senhas.FindIndex(senha => senha == senhaParaApresentar);

                if (indexSenhaConsultar == -1)
                {
                    Console.WriteLine("Não foi possível encontrar esta senha");
                    Console.WriteLine("Confira a Senha e tente novamente.");
                }
                else
                {
                    Console.Write("\nDigite a quantia do Saque: R$ ");
                    double quantiaSaque = double.Parse(Console.ReadLine());
                    double quantiaSaldoAtualizado = saldos[indexSenhaConsultar] - quantiaSaque;

                    if (quantiaSaldoAtualizado >= 0)
                    {
                        Console.WriteLine($"Valor do saldo atualizado: R$ {quantiaSaldoAtualizado}");
                        saldos[indexParaConsultar] = quantiaSaldoAtualizado;
                        Console.WriteLine("Saque realizado.");
                    }
                    else
                    {
                        Console.WriteLine($"Valor do saldo atualizado: R$ {quantiaSaldoAtualizado}");
                        Console.WriteLine("Saldo insuficiente");
                        Console.WriteLine($"Seu saldo atual: R$ {saldos[indexParaConsultar]:f2}.");

                    }

                    Console.WriteLine("Pressione Enter para continuar.");
                    Console.ReadKey();
                }
            }
        }

        static void RealizarDeposito(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Write("\nDigite o cpf: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaConsultar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaConsultar == -1)
            {
                Console.WriteLine("Não foi possível encontrar esta conta");
                Console.WriteLine("Confira o CPF e tente novamente.");
            }
            else
            {
                Console.Write("\nDigite a quantida do deposito: R$ ");
                saldos[indexParaConsultar] += double.Parse(Console.ReadLine());
                Console.WriteLine("Depósito realizado.");
                Console.WriteLine("Pressione Enter para continuar.");
                Console.ReadKey();
            }
        }

        static void RealizarTransferencia(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("\nDigite o seu cpf: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaConsultar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaConsultar == -1)
            {
                Console.WriteLine("Não foi possível encontrar esta conta");
                Console.WriteLine("Confira o CPF e tente novamente.");
            }
            else
            {
                Console.Write("\nDigite o cpf do Destinatário: ");
                string cpfDestinatário = Console.ReadLine();
                int indexDestinatario = cpfs.FindIndex(cpf => cpf == cpfDestinatário);

                if (indexDestinatario == -1)
                {
                    Console.WriteLine("\nConta do destinatário não encontrada");
                    Console.WriteLine("Confira o CPF e tente novamente.");
                }
                else
                {
                    Console.Write("\nDigite a senha da conta: ");
                    string senhaParaApresentar = GetPassword();
                    int indexSenhaConsultar = senhas.FindIndex(senha => senha == senhaParaApresentar);

                    if (indexSenhaConsultar == -1)
                    {
                        Console.WriteLine("Não foi possível encontrar esta senha");
                        Console.WriteLine("Confira a Senha e tente novamente.");
                    }
                    else
                    {
                        Console.Write("\nDigite o valor da transferência: R$ ");
                        double valor = double.Parse(Console.ReadLine());

                        if (saldos[indexParaConsultar] - valor >= 0)
                        {
                            saldos[indexParaConsultar] -= valor;
                            saldos[indexDestinatario] += valor;
                            Console.WriteLine("\nTransferência para o Destinatário realizado.");
                        }
                        else
                        {
                            Console.WriteLine("\nSaldo insuficiente");
                            Console.WriteLine($"Seu Saldo atual: R$ {saldos[indexParaConsultar]:f2}");
                        }

                        Console.WriteLine("Pressione Enter para continuar.");
                        Console.ReadKey();
                    }
                }                
            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Antes de usar, vamos configurar alguns valores: ");

            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

            int option;

            do
            {
                ShowMenu();
                option = int.Parse(Console.ReadLine());

                Console.WriteLine("_______________________________________");

                switch (option)
                {
                    case 0:
                        Console.WriteLine("Estou encerrando o programa...");
                        break;

                    case 1:
                        RegistrarNovoUsuario(cpfs, titulares, senhas, saldos);
                        break;

                    case 2:
                        DeletarUsuario(cpfs, titulares, senhas, saldos);
                        break;

                    case 3:
                        ListarTodasAsContas(cpfs, titulares, saldos);
                        break;

                    case 4:
                        ApresentarUsuario(cpfs, titulares, saldos);
                        break;
                    case 5:
                        QuantiaArmazenada(cpfs, titulares, saldos);
                        break;
                    case 6:
                        Console.Write("\nDigite sua senha para fazer Login neste terminal: ");
                        string senhaParaApresentar = GetPassword();
                        int indexSenhaConsultar = senhas.FindIndex(senha => senha == senhaParaApresentar);

                        do
                        {
                            ManipularConta(cpfs, titulares, senhas, saldos);

                            option = int.Parse(Console.ReadLine());

                            switch (option)
                            {
                                case 1:
                                    RealizarDeposito(cpfs, titulares, saldos);
                                    break;
                                case 2:
                                    RealizarSaque(cpfs, titulares, senhas, saldos);
                                    break;
                                case 3:
                                    RealizarTransferencia(cpfs, titulares, senhas, saldos);
                                    break;
                                case 9:
                                    break;
                            }
                        }
                        while (option != 9);
                        break;
                }

                Console.WriteLine("_______________________________________");

            }
            while (option != 0);
        }
    }
}
