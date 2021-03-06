﻿using System;
using System.Collections.Generic;

namespace Domaci
{
	class Primer
	{
		public int br;
		public Primer(int x)
		{
			br = x;
		}
	}

	class Program
	{
		static List<Artikal> Artikli = new List<Artikal>();
		static List<Racun> Racuni = new List<Racun>();

		static void Main(string[] args)
		{
			//Artikal a = new Artikal();
			//a.Kolicina = 0;

			//checked     -- Stiti od overflowa :) 
			//{
			//	a.Kolicina -= 20;
			//}
			string unos;
			do
			{
				Meni();
				unos = Console.ReadKey().KeyChar.ToString();
				Console.WriteLine();
				switch (unos)
				{
					case "1":
						Unos();
						break;
					case "2":
						Ispis();
						break;
					case "3":
						Brisanje();
						break;
					case "4":
						Izmena();
						break;
					case "5":
						Izdavanje();
						break;
					case "6":
						IspisRacuna();
						break;
					case "7":
						Console.WriteLine("Bye :) ");
						Console.ReadKey();
						break;
				}
			} while (unos != "7");
		}

		static void Izdavanje()
		{
			//TODO Popraviti bugove u metodi :D

			//TODO Ako unesemo artikal koji vec postoji na racunu, treba da samo uvecamo kolicinu 
			//vec postojeceg a nikako da dodamo jos jednom

			//TODO Kao i za unos artikla, ne izbacivati korisnika na meni kada pogresi
			//Takdoje, nemamo nikakvu kontrolu da li uposte ima kolicine koja se trazi
			//Tip: Mozda da, po unosu sifre, prikazemo trenutnu kolicinu odmah? :) 
			Racun r = new Racun();
			r.Rbr = (uint)Racuni.Count + 1;

			do
			{
				Console.Write("Unesite sifru artikla: ");
				string sifra = Console.ReadLine();

				foreach (Artikal a in Artikli)
				{
					if (a.Sifra == sifra)
					{
						Console.WriteLine("Kolicina na raspolaganju :" + a.Kolicina);
						int kol;
						do
						{
							Console.Write("Unesite kolicinu: ");
							kol = int.Parse(Console.ReadLine());
						} while (a.Kolicina - kol < 0);

						if (r.Art.Contains(a))
						{
							r.Kolicina[r.Art.IndexOf(a)] += kol;
						}
						else
						{
							r.Art.Add(a);
							r.Kolicina.Add(kol);
						}

						a.Kolicina -= kol;
						//a.Kolicina = r.Kolicina[r.Kolicina.Count - 1]; Uzimamo zadnju stvar sa liste

						Console.Write("Unosite jos artikala? (d/n): ");
						string unos = Console.ReadKey().KeyChar.ToString();
						Console.Write("\n");
						if (unos == "n")
						{
							Racuni.Add(r);
							return;
						}
						if(unos == "d")
                        {
							break;
                        }
					}
					
				}

			} while (true);
		}

		static void Brisanje()
		{
			Console.Write("Unesite sifru: ");
			string sifra = Console.ReadLine();

			Artikal zaBrisanje = null;
			foreach (Artikal a in Artikli)
			{
				if (a.Sifra == sifra)
				{
					zaBrisanje = a;
					break;
				}
			}

			if (Artikli.Remove(zaBrisanje))
			{
				Console.WriteLine("Uspesno obrisano!");
			}
			else
			{
				Console.WriteLine("Sifra nije pronadjena!");
			}
		}

		static void Ispis()
		{
			Console.WriteLine("=============================");
			foreach (Artikal a in Artikli)
			{
				Console.WriteLine($"{a.Sifra} -- {a.Naziv} {a.Ucena} {a.MarzaProc}% {a.Kolicina} {a.IzracunajIzlaznu()}");
			}
			Console.WriteLine("=============================");
		}

		static void IspisRacuna()
		{
			Console.WriteLine("=============================");
			foreach (Racun r in Racuni)
			{
				Console.WriteLine($"Rbr. : {r.Rbr}");
				Console.WriteLine("-------------------------");
				for (int indeks = 0; indeks < r.Art.Count; indeks++)
				{
					Console.WriteLine($"|{r.Art[indeks].Sifra}-{r.Art[indeks].Naziv}|{r.Kolicina[indeks]}|{r.Art[indeks].IzracunajIzlaznu() * r.Kolicina[indeks]}");
				}
				Console.WriteLine("-------------------------");
			}
			Console.WriteLine("=============================");
		}

		static void Unos()
		{
			//TODO Za domaci napraviti unos da je kulturan :) tj, da korisnika ne izbacuje na glavni meni
			//nego da mu kaze da je pogresio i ponudi mu da ponovo unese istu stvar
			string sifra, naziv;

			while (true)
			{
				Console.Write("Unesite novu sifru: ");
				sifra = Console.ReadLine();
				bool posotji = false;
				foreach (Artikal a in Artikli)
				{
					if (a.Sifra == sifra)
					{
						Console.WriteLine("Sifra vec postoji :(");
						posotji = true;
						break;
					}
				}

				if (!posotji)
					break;
			}

			while (true)
			{
				Console.Write("Unesite novi naziv: ");
				naziv = Console.ReadLine();
				bool postoji = false;

				foreach (Artikal a in Artikli)
				{
					if (a.Naziv == naziv)
					{
						Console.WriteLine("Naziv vec postoji :(");
						postoji = true;
						break;
					}
				}

				if (!postoji)
					break;
			}


			//TODO cena i marza ne mogu da budu negativne, niti 0
			decimal ucena;
			int marza, kolicina;
			do
			{
				Console.Write("Unesite ulaznu cenu: ");
				ucena = decimal.Parse(Console.ReadLine());
			} while (ucena <= 0);

			do
			{
				Console.Write("Unesite marzu: ");
				marza = int.Parse(Console.ReadLine());
			} while (marza <= 0);

			do
			{
				Console.Write("Unesite kolicinu: ");
				kolicina = int.Parse(Console.ReadLine());
			} while (kolicina <= 0);


			Artikli.Add(new Artikal(sifra, naziv, ucena, marza, kolicina));
		}

		static void Meni()
		{
			Console.WriteLine("1 - Dodavanje");
			Console.WriteLine("2 - Stampanje");
			Console.WriteLine("3 - Brisanje");
			Console.WriteLine("4 - Izmena"); // TODO Domaci :P 
											 //Napraviti izmenu tako da korisnik unese sifru za artikal. Ako smo ga nasli, ponuditi korisniku
											 //da izmeni naziv, ulaznu cenu, marzu ili kolicinu.
			Console.WriteLine("5 - Racun");
			Console.WriteLine("6 - Izlistaj racune");
			Console.WriteLine("7 - Izlaz");
			Console.WriteLine("------------------");
			Console.Write("Izbor: ");
		}

		static void Izmena()
		{
			Console.Write("Unesite sifru :");
			string sifra = Console.ReadLine();

			foreach (Artikal a in Artikli)
			{
				if (sifra == a.Sifra)
				{
					Console.Write("Unesite 1 za promenu naziva, 2 za ulaznu cenu, 3 za marzu, 4 za kolicinu :");
					char unos = Console.ReadKey().KeyChar;
					Console.Write("\n");

					if (unos == '1')
					{
						while (true)
						{
							Console.Write("Unesite novi naziv: ");
							string naziv = Console.ReadLine();
							bool postoji = false;

							foreach (Artikal b in Artikli)
							{
								if (b.Naziv == naziv)
								{
									Console.WriteLine("Naziv vec postoji :(");
									postoji = true;
									break;
								}
							}

							if (!postoji)
							{
								a.Naziv = naziv;
								break;
							}
						}
					}

					if (unos == '2')
					{
						int cena;
						do
						{
							Console.Write("Unesite novu ulaznu cenu :");
							cena = int.Parse(Console.ReadLine());
						} while (cena <= 0);

						a.Ucena = cena;
					}

					if (unos == '3')
					{
						int marza;
						do
						{
							Console.Write("Unesite novu marzu");
							marza = int.Parse(Console.ReadLine());
						} while (marza <= 0);
					}

					if (unos == '4')
					{
						int kolicina;
						do
						{
							Console.Write("Unesite novu kolicinu");
							kolicina = int.Parse(Console.ReadLine());
						} while (kolicina <= 0);
					}

					break;

				}

			}
		}

	}


	class Racun
	{
		public uint Rbr;

		public List<Artikal> Art = new List<Artikal>();
		public List<int> Kolicina = new List<int>();

		public decimal DajTotal()
		{
			return 0;//Art[0].IzracunajIzlaznu() * Kolicina;
		}
	}

	class Artikal
	{
		public string Sifra;
		public string Naziv;
		public decimal Ucena;
		public int MarzaProc;
		public int Kolicina;

		public decimal IzracunajIzlaznu()
		{
			return Ucena * (1 + (decimal)MarzaProc / 100);
		}

		public Artikal(string s, string n, decimal c, int m, int k)
		{
			Sifra = s;
			Naziv = n;
			Ucena = c;
			MarzaProc = m;
			Kolicina = k;
		}
	}
}
