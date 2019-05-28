using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacao;
using Microsoft.AspNet;
using Newtonsoft.Json;
using System.IO;
using Dominio;
using System.Web.Http;
using Loja.Models.Carrinho;
using Core;
using Newtonsoft.Json.Linq;

namespace Loja.Controllers
{
    public class ValuesController : ApiController
    {

        // GET api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/devil/analise/{tip}/{code}/{codd}/{codee}/{codde}/{co}/{cod}")]
        [HttpGet]
        public chartsjs Get(int tip, int code, int codd, int codee, int codde, int co, int cod)
        {
            //qtd do eixo?
            if (codd < 12)
                codd++;
            else
            {
                code++;
                codd = 1;
            }
            //code ano max
            //codee ano min
            //codd mes max
            //codde mes min
            DateTime dt_max = new DateTime(code, codd, 1);
            DateTime dt_min = new DateTime(codee, codde, 1);
            Dominio.Analise analise = new Dominio.Analise() { Data_max = dt_max, Data_min = dt_min };
            analise.Id = cod;
            Random rnd = new Random();
            if (analise.Id == 0)
            {
                Dominio.data asd = new Dominio.data()
                {
                    //eixo x do grafico
                    labels = new string[] { "Janeiro", "Fevereiro", "Março", "Abril", "Maio"},

                };
                var fdsa = new List<Dominio.datasets>() { };
                //qtd do eixo!!
                for (int i = 0; i < analise.resultado.Keys.Count; i++)
                {
                    if (co == 0)
                    {
                        //qts pedidos dentro da lista?
                        List<Pedido> b = analise.p;
                        // cor randomica
                        var color = "rgb(" + rnd.Next(0, 255) + "," + rnd.Next(0, 255) + " , " + rnd.Next(0, 255) + ")";
                        var go = new datasets() { label = analise.resultado.Keys.ElementAt(i), backgroundColor = (string)color.Clone(), borderColor = (string)color.Clone(), fill = false };
                        var grr = new List<double>() { };

                        //eixo y
                        var queen = new double[12];
                        //foreach dentro de pedido
                        foreach (Pedido bbb in b)
                        {
                            if (bbb.Usuario == analise.resultado.Keys.ElementAt(i))
                            {
                                double devil = 0;
                                //pedido por mes, contador de pedidos por mes
                                switch (bbb.DataPedido.Month)
                                {
                                    case 1:
                                        queen[0]++;
                                        break;
                                    case 2:
                                        queen[1]++;
                                        break;
                                    case 3:
                                        queen[2]++;
                                        break;
                                    case 4:
                                        queen[3]++;
                                        break;
                                    case 5:
                                        queen[4]++;
                                        break;
                                    case 6:
                                        queen[5]++;
                                        break;
                                    case 7:
                                        queen[6]++;
                                        break;
                                    case 8:
                                        queen[7]++;
                                        break;
                                    case 9:
                                        queen[8]++;
                                        break;
                                    case 10:
                                        queen[9]++;
                                        break;
                                    case 11:
                                        queen[10]++;
                                        break;
                                    case 12:
                                        queen[11]++;
                                        break;
                                }
                            }
                            
                        }
                        grr.AddRange(queen);
                        go.data = grr.ToArray();
                        fdsa.Add(go);
                    }
                    else if (co == i + 1)
                    {
                        List<Pedido> b = analise.p;
                        var color = "rgb(" + rnd.Next(0, 255) + "," + rnd.Next(0, 255) + " , " + rnd.Next(0, 255) + ")";
                        var go = new datasets() { label = analise.resultado.Keys.ElementAt(i), backgroundColor = (string)color.Clone(), borderColor = (string)color.Clone(), fill = false };
                        var grr = new List<double>() { };
                        //eixo y
                        var queen = new double[12];
                        foreach (Pedido bbb in b)
                        {                           
                            if (bbb.Usuario == analise.resultado.Keys.ElementAt(i))
                            {
                                double devil = 0;
                                switch (bbb.DataPedido.Month)
                                {
                                    case 1:
                                        queen[0]++;
                                        break;
                                    case 2:
                                        queen[1]++;
                                        break;
                                    case 3:
                                        queen[2]++;
                                        break;
                                    case 4:
                                        queen[3]++;
                                        break;
                                    case 5:
                                        queen[4]++;
                                        break;
                                    case 6:
                                        queen[5]++;
                                        break;
                                    case 7:
                                        queen[6]++;
                                        break;
                                    case 8:
                                        queen[7]++;
                                        break;
                                    case 9:
                                        queen[8]++;
                                        break;
                                    case 10:
                                        queen[9]++;
                                        break;
                                    case 11:
                                        queen[10]++;
                                        break;
                                    case 12:
                                        queen[11]++;
                                        break;
                                }
                            }

                        }
                        grr.AddRange(queen);
                        go.data = grr.ToArray();
                        fdsa.Add(go);

                    }
                }
                asd.datasets = fdsa.ToArray();

                Dominio.options asdf = new Dominio.options()
                {
                    //titulo do grafico
                    responsive = true,
                    title = new title() { display = true, text = "Quantidade de Vendas (ano " + analise.Data_min.Year + ")" },
                    tooltips = new tooltips() { intersect = false, mode = "index" },
                    hover = new hover() { intersect = true, mode = "nearest" },
                    //Legenda das escalas qtd por mês
                    scales = new scales()
                    {
                        xAxes = new xAxes[] { new xAxes() { display = true, scaleLabel = new scaleLabel() { display = true, labelString = "Meses" } } },
                        yAxes = new yAxes[] { new yAxes() { display = true, scaleLabel = new scaleLabel() { display = true, labelString = "Quantidade" } } }
                    }
                };
                if (tip == 0)
                    analise.chartsjs.type = "line";
                else
                    analise.chartsjs.type = "bar";
                analise.chartsjs.data = asd;
                analise.chartsjs.options = asdf;
            }
            
             
            return analise.chartsjs;//System.IO.File.ReadAllText("./analise.json");
        }
        // GET api/<controller>/5
        //[HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}