﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMC.DATA;
using TMC.DAL.Interfaces;
using FireSharp;
using FireSharp.Response;
using Newtonsoft.Json;

namespace TMC.DAL.Metodos
{ 
    public class MComprasDAL : PrincipalBD, IComprasDAL
    {
        
        public void Actualizar(TbCompras compra)
        {
            try
            {
                client.UpdateAsync("TbCompras/" + compra.IDCompra, compra);
            }
            catch { };
        }

        public TbCompras Buscar(int idCompra)
        {
            var response = client.Get("TbCompras/" + idCompra);
            TbCompras valor = JsonConvert.DeserializeObject<TbCompras>(response.Body);
            return valor;
        }
        
        public void Eliminar(int idCompra)
        {
            try
            {
                var busqueda = Buscar(idCompra);
                busqueda.estado = false;
                Actualizar(busqueda);
            }
            catch { };
        }

        public void cancelarServicio(int idUsuario, int idServicio)
        {
            var id = 0;
            var listaServicios = new MServiciosDAL().obtenerServiciosComprados(idUsuario);
            foreach (var servicio in listaServicios)
            {
                //de listaServicios, averiguar quien tiene el mismo idUsuario, idServicio y obtener el idCompra.
                //asignarle el idCompra a la variable id.
            }
            client.DeleteAsync("TbUsuarios_TbServicios/"  + id);
        }


        public void Insertar(TbCompras compra)
        {
            try
            {
                var lista = Mostrar();
                if (lista != null)
                {
                    compra.IDCompra = lista[lista.Count() - 1].IDCompra + 1;
                }
                else
                {
                    compra.IDCompra = 1;
                };
                client.SetAsync("TbCompras/" + compra.IDCompra, compra);

            }
            catch
            {

            }
        }

        public List<TbCompras> Mostrar()
        {
            var response = client.Get("TbCompras");
            TbCompras[] tabla = JsonConvert.DeserializeObject<TbCompras[]>(response.Body);
            if (tabla == null) { return null; }
            var lista = new List<TbCompras>();
            foreach (var item in tabla)
            {
                if (item != null)
                {
                    lista.Add(item);
                }
            }

            return lista;
        }

        public List<TbServicios> ObtenerComprasId(int Id)
        {
            List<TbServicios> serviciosContratados = new List<TbServicios>();
            var response = client.Get("TbUsuarios_TbServicios/");
            TbUsuario_TbServicio[] servicios = { };
            servicios = JsonConvert.DeserializeObject<TbUsuario_TbServicio[]>(response.Body);
            foreach (var servicio in servicios)
            {
                if (servicio != null && servicio.idUsuario == Id)
                {
                    serviciosContratados.Add(new MServiciosDAL().Buscar(servicio.idServicio));
                }
            }
            return serviciosContratados;
        }


    }
}
