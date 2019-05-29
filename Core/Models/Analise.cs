﻿using Core;
using Loja.Models;
using Loja.Models.Carrinho;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    
    public class datasets
    {
        public string label;
        public string backgroundColor;
        public string borderColor;
        public double[] data;
        public bool fill;
    }
    public class data
    {
        public string[] labels;
        public datasets[] datasets;
    }
    public enum tipo_grafico
    {
        line,
        pizza,
        barras
    }

    public class title
    {
        public bool display;
        public string text;

    }
    public class tooltips
    {
        public bool intersect;
        public string mode;

    }
    public class hover
    {
        public bool intersect;
        public string mode;

    }
    public class scaleLabel
    {
        public bool display;
        public string labelString;
    }
    public class xAxes
    {
        public bool display;
        public scaleLabel scaleLabel;
    }
    public class yAxes
    {
        public bool display;
        public scaleLabel scaleLabel;
        public ticks ticks = new ticks();
    }
    public class ticks
    {
        public bool beginAtZero = true;
    }
    public class scales
    {
        public xAxes[] xAxes;
        public yAxes[] yAxes;
    }
    public class options
    {
        public bool responsive = true;
        public title title;
        public tooltips tooltips;
        public hover hover;
        public scales scales;
    }
    public class chartsjs
    {
        public string type;
        public data data;
        public options options;
        public chartsjs()
        {
            data = new data();
            type = Enum.GetName(typeof(tipo_grafico), tipo_grafico.line);
        }
    }
    public class Analise : EntidadeDominio
    {
        private ApplicationDbContext storeDB = new ApplicationDbContext();
        //public List<Pedido> p;
        public List<DetalhesPedido> prod;
        public List<Produto> pa;
        private DateTime data_max;

        public DateTime Data_max
        {
            get { return data_max; }
            set { data_max = value; }
        }

        private DateTime data_min;

        public DateTime Data_min
        {
            get { return data_min; }
            set { data_min = value; }
        }

        public string[] generic_labels;
        public Dictionary<string, List<DetalhesPedido>> resultado;
        public Dictionary<string, List<Produto>> resultado2;
        public chartsjs chartsjs;

        public Analise() : base()
        {
            data_min = data_max = DateTime.Now;
            chartsjs = new chartsjs();
            //qtd de objetos de pedidos
            //p = storeDB.Pedidoes.ToList();
            prod = storeDB.DetalhesPedidoes.ToList();
            resultado = new Dictionary<string, List<DetalhesPedido>>();
            resultado2 = new Dictionary<string, List<Produto>>();
            //foreach (string b in p.DistinctBy(mbox => mbox.Usuario).Select(mbox => mbox.Usuario).ToList())
            //  resultado.Add(b, new List<Pedido>());

            /*foreach (string item in pa.DistinctBy(mbox => mbox.Titulo).Select(mbox => mbox.Titulo).ToList())
            {
                resultado2.Add(pa.ToString(), new List<Produto>());
            }
            */
            foreach (int a in prod.DistinctBy(mbox => mbox.ProdutoId).Select(mbox => mbox.ProdutoId).ToList())
            {
                
                resultado.Add(a.ToString(), new List<DetalhesPedido>());
            }
        }
    }
}