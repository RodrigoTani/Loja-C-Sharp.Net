using System;
using System.Collections.Generic;
using System.Text;
using Core.Core;
using Dominio;
using Core.DAO;
using System.Linq;
using Loja.Models.Carrinho;

namespace Core.Negocio
{
    class inteligencia 
    {
        //public string processar(DateTime min,DateTime max)
        //{
            /*
            Analise ana = new Analise();
            List<string> lbls = new List<string>();
            if (ana.Id == 0)
            {
                //BagagemDAO go = new BagagemDAO();
                var dii = ana.storeDB.DetalhesPedidoes.Join(ana.storeDB.Pedidoes,
                                                            person => person.PedidoId,
                                                            pet => pet.PedidoId,
                                                            (person, pet) =>
                                                            new { DetalhesPedido = person, Pedido = pet })
                                                            .Where(ddd => ddd.Pedido.DataPedido >= min && ddd.Pedido.DataPedido <= max).ToList();
                //dii= dii.OrderBy(x => ((Bagagem)x).dono.Passagem.Voo.DT_partida).ToList();
                foreach (var b in dii)
                {
                    string c = b.DetalhesPedido.ProdutoId.ToString();
                    if (!ana.resultado.ContainsKey(c))
                    {
                        ana.resultado.Add(c, new List<object>() { b.DetalhesPedido });
                    }
                    else
                    {
                        ana.resultado[c].Add(b.DetalhesPedido);
                    }
                }
                for (int i = 0; i < ana.resultado.Keys.Count; i++)
                {
                    List<object> b = ana.resultado.Values.ElementAt(i);
                    List<object> bb = new List<object>();

                    foreach (var cc in b)
                    {
                        if (bb.Count == 0)
                        {
                            bb.Add(cc);
                            continue;
                        }
                       
                        if (cc.dono.Passagem.Voo.DT_partida.ToString("MM/yyyy") == ((Bagagem)bb.ElementAt(bb.Count - 1)).dono.Passagem.Voo.DT_partida.ToString("MM/yyyy")
                            && cc.dono.Passagem.Voo.LO_partida.sigla == ((Bagagem)bb.ElementAt(bb.Count - 1)).dono.Passagem.Voo.LO_partida.sigla
                            && cc.dono.Passagem.Voo.LO_chegada.sigla == ((Bagagem)bb.ElementAt(bb.Count - 1)).dono.Passagem.Voo.LO_chegada.sigla)
                            ((Bagagem)bb.ElementAt(bb.Count - 1)).peso += cc.peso;
                        else
                            bb.Add(cc);
                    }
                    ana.resultado[ana.resultado.Keys.ElementAt(i)] = bb;
                }
                if (ana.resultado.Values.Count > 0)
                {
                    foreach (Pedido b in ana.resultado.Values.ElementAt(0))
                    {
                        lbls.Add(b.dono.Passagem.Voo.DT_partida.ToString("MMMM"));
                    }
                    ana.generic_labels = lbls.ToArray();
                }
                return null;
                //throw new NotImplementedException();
            }
             */
        //}
    }
}
