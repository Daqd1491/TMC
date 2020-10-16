﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMC.DATA;

namespace TMC.DAL.Interfaces
{
    public interface IPermisosDAL
    {
        //Insert del registro en la DB
        void Insertar(TbPermisos permiso);
        //Update registro en la DB
        void Actualizar(TbPermisos permiso);
        //Elimina un registro de la DB
        void Eliminar(int idPermiso);
        //Lista con todos los registros
        List<TbPermisos> Mostrar();
        //Busca un registro especifico
        TbPermisos Buscar(int idPermiso);
    }
}
