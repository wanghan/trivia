using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trivia;
using System.IO;
using System.Threading;
using System.Text;
using System.Resources;

namespace TriviaUnitTest
{
	[TestClass]
	public class GolderMasterTest
	{
		TextWriter tempConsoleOut = null;

		[TestInitialize]
		public void InitialTest()
		{
			tempConsoleOut = Console.Out;

		}

		[TestCleanup]
		public void CleanupTest()
		{
			if (tempConsoleOut != null)
			{
				Console.SetOut(tempConsoleOut);
			}
		}

		//[TestMethod]
		public void Test_Output_Once()
		{
			int seed = 1;
			string output1 = this.GenerateOutput(seed);
			Thread.Sleep(100);
			string output2 = this.GenerateOutput(seed);

			Assert.AreEqual(output1, output2);
		}

		[TestMethod]
		public void Test_Golder_Master()
		{
			this.GenerateManyToFile(20, "tem1.txt");
			
			Assert.AreEqual(this.ReadFileContent("tem1.txt"), this.ReadFileContent("GolderMaster_20.txt"));
		}

		//[TestMethod]
		public void Test_FileOutput_Many()
		{
			this.GenerateManyToFile(20, "tem1.txt");
			this.GenerateManyToFile(20, "tem2.txt");

			Assert.AreEqual(this.ReadFileContent("tem1.txt"), this.ReadFileContent("tem2.txt"));
		}

		private void GenerateManyToFile(int times, string filename)
		{
			using (FileStream fs = new FileStream(filename, FileMode.Create))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					for (int i = 0; i < times; ++i)
					{
						string output = this.GenerateOutput(times);
						sw.Write(output);
						sw.Flush();
					}
				}
			}
		}

		private string ReadFileContent(string filename)
		{
			StringBuilder sb = new StringBuilder();
			using (StreamReader sr = new StreamReader(filename))
			{
				sb.AppendLine(sr.ReadToEnd());
			}

			return sb.ToString();
		}

		/// <summary>
		/// Run game runner once and get the console output.
		/// </summary>
		/// <returns></returns>
		private string GenerateOutput(int seed)
		{
			string output = null;
			using (MemoryStream ms = new MemoryStream())
			{
				StreamWriter sw = new StreamWriter(ms);
				Console.SetOut(sw);

				GameRunner runner = new GameRunner(seed);
				runner.Run();

				sw.Flush();
				ms.Seek(0, SeekOrigin.Begin);

				StreamReader sr = new StreamReader(ms);
				output = sr.ReadToEnd();
			}

			return output;
		}
	}
}
