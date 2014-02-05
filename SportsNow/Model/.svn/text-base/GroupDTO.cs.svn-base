using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.SportsNow.Model
{
    public class GroupDTO
    {
        public int NumMiembros { get; set; }

        public int NumRecomendaciones { get; set; }

        public long GroupId { get; set; }

        public string GroupName { get; set; }

        public string GroupDescription { get; set; }

        public GroupDTO(UserGroup userGroup, int recomendaciones, int miembros)
        {
            GroupId = userGroup.id;
            GroupName = userGroup.name;
            GroupDescription = userGroup.descrip;
            NumRecomendaciones = recomendaciones;
            NumMiembros = miembros;
        }
    }
}
