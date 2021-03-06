//  All code copyright (c) 2003 Barry Lapthorn
//  Website:  http://www.lapthorn.net
//
//  Disclaimer:  
//  All code is provided on an "AS IS" basis, without warranty. The author 
//  makes no representation, or warranty, either express or implied, with 
//  respect to the code, its quality, accuracy, or fitness for a specific 
//  purpose. Therefore, the author shall not have any liability to you or any 
//  other person or entity with respect to any liability, loss, or damage 
//  caused or alleged to have been caused directly or indirectly by the code
//  provided.  This includes, but is not limited to, interruption of service, 
//  loss of data, loss of profits, or consequential damages from the use of 
//  this code.
//
//
//  $Author: barry $
//  $Revision: 1.1 $
//
//  $Id: Genome.cs,v 1.1 2003/08/19 20:59:05 barry Exp $


using System;
using System.Collections;
using btl.generic;
using System.Text;

namespace btl.generic
{
	/// <summary>
	/// Summary description for Genome.
	/// </summary>
	public class Genome
	{

        public double[] m_genes;
        public string[] m_word_genes;
        private string s_genes;

        public string S_genes
        {
            get { return s_genes; }
            set { s_genes = value; }
        }
        private int m_length;
        private double m_fitness;
        static Random m_random = new Random();




		public Genome()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public Genome(int position, double stringLength)
		{
            //m_length = length;
            //m_genes = new double[ length ];
            //CreateGenes();
            int bitLength = (int)Math.Log(stringLength, 2);
            string binary = Convert.ToString(position, 2);
            int i = 0;
            string prefix = "";
            while (i <= bitLength - binary.Length) {
                prefix += "0";
                i++;
            }
            s_genes = prefix + binary;
		}
		public Genome(int length, bool createGenes)
		{
			m_length = length;
			m_genes = new double[ length ];
			if (createGenes)
				CreateGenes();
		}

		public Genome(ref double[] genes)
		{
			m_length = genes.GetLength(0);
			m_genes = new double[ m_length ];
			for (int i = 0 ; i < m_length ; i++)
				m_genes[i] = genes[i];
		}

        public Genome(string gene)
        {
            m_length = gene.Length;
            m_word_genes = new string[m_length];
            CreateCharacterGenes(gene);
        }
		 

		private void CreateGenes()
		{
			// DateTime d = DateTime.UtcNow;
			for (int i = 0 ; i < m_length ; i++)
				m_genes[i] = m_random.NextDouble();
		}

        public void CreateCharacterGenes(string gene)
        {
            for (int i = 0; i < m_length; i++)
            {
                m_word_genes[i] = gene[i].ToString();
            }
        }

		public void Crossover(ref Genome genome2, out Genome child1, out Genome child2)
		{
			int pos = (int)(m_random.NextDouble() * (double)m_length);
			child1 = new Genome(m_length, false);
			child2 = new Genome(m_length, false);
			for(int i = 0 ; i < m_length ; i++)
			{
				if (i < pos)
				{
					child1.m_genes[i] = m_genes[i];
					child2.m_genes[i] = genome2.m_genes[i];
				}
				else
				{
					child1.m_genes[i] = genome2.m_genes[i];
					child2.m_genes[i] = m_genes[i];
				}
			}
		}

        public void Crossover_s(ref Genome genome2, out Genome child1, out Genome child2)
        {
            Random rd = new Random();
            child1 = new Genome();
            child2 = new Genome();
            int pos = (int)rd.Next(0, genome2.s_genes.Length - 1);
            pos = (int)genome2.s_genes.Length / 2;
            int lenght = genome2.S_genes.Length;
            string genome_t1 = s_genes;
            string genome_t2 = genome2.s_genes;
            child1.S_genes = genome_t1.Substring(0, pos + 1) + genome_t2.Substring(pos + 1, lenght - pos - 1);
            genome_t1 = s_genes;
            genome_t2 = genome2.S_genes;
            child2.S_genes = genome_t2.Substring(0, pos + 1) + genome_t1.Substring(pos + 1, lenght - pos - 1);
        }


		public void Mutate()
		{
			for (int pos = 0 ; pos < m_length; pos++)
			{
				if (m_random.NextDouble() < m_mutationRate)
					m_genes[pos] = (m_genes[pos] + m_random.NextDouble())/2.0;
			}
		}

        public void Mutate_s()
        {
            Random rd = new Random();
            int pos = (int)rd.Next(0, s_genes.Length - 1);
            StringBuilder sb = new StringBuilder(S_genes);
            if (m_random.NextDouble() < m_mutationRate)
            {
                if (S_genes[pos] == '1')
                {
                    sb[pos] = '0';
                }
                else
                {
                    sb[pos] = '1';
                }
            }
            
            S_genes = sb.ToString();
        }

		public string Genes()
		{
			return s_genes;
		}

		public void Output()
		{
			for (int i = 0 ; i < m_length ; i++)
			{
				System.Console.WriteLine("{0:F4}", m_genes[i]);
			}
			System.Console.Write("\n");
		}

		public void GetValues(ref double[] values)
		{
			for (int i = 0 ; i < m_length ; i++)
				values[i] = m_genes[i];
		}

		

		private static double m_mutationRate;
        private string p;

		public double Fitness
		{
			get
			{
				return m_fitness;
			}
			set
			{
				m_fitness = value;
			}
		}




		public static double MutationRate
		{
			get
			{
				return m_mutationRate;
			}
			set
			{
				m_mutationRate = value;
			}
		}

		public int Length
		{
			get
			{
				return m_length;
			}
		}
	}
}
