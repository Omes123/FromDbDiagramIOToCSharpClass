using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FromDbDiagramIOToCSharpClass
{
    class Program
    {
        static void Main(string[] args)
        {
            String def_namespace = "ExportCelco";
            List<String> fileLines = File.ReadAllLines("input.txt").Where(s=>!String.IsNullOrEmpty(s.Trim())).ToList();
            for(int i = 0; i < fileLines.Count(); i++)
            {
                if (fileLines[i].Trim().StartsWith("Table"))
                {
                    List<String> newClass = new List<string>();
                    newClass.Add("using System;");
                    newClass.Add("");
                    newClass.Add("namespace " + def_namespace);
                    newClass.Add("{");
                    String[] temp = fileLines[i].Trim().Split(' ');
                    temp[1] = temp[1].Replace("{", "");
                    newClass.Add("  class " + temp[1]);
                    newClass.Add("  {");

                    List<String> classFields=new List<string>();
                    while (fileLines[i] != "}")
                    {
                        
                        String[] variable_descriptors = fileLines[i].Trim().Split(" ");
                        String type;
                        switch (variable_descriptors[1])
                        {
                            case "int":
                                type = "int";
                                break;
                            case "datetime":
                                type = "DateTime";
                                break;
                            case "char":
                                type = "char";
                                break;
                            default:
                                type = "String";
                                break;
                        }
                        newClass.Add("      public "+type+variable_descriptors[0]+@" { get; set; }");
                        classFields.Add(variable_descriptors[0]);
                        i++;
                    }
                    newClass.Add("      public override string ToString()");
                    newClass.Add("      {");
                    newClass.Add("          return " +String.Join(" + \",\" + ",classFields)+";");
                    newClass.Add("  }");
                    newClass.Add("}");
                    File.WriteAllLines(temp[1] + ".cs",newClass);
                }
            }
        }

        /*
         * 
        Table CodSiruta  {
            Cod_Siruta int [pk,increment]
            Denumire_unitate varchar
            Cod_Judet int
            Denumire_Judet varchar
            Cod_UAT int
            Nume_UAT varchar
        }
         */
    }
}
