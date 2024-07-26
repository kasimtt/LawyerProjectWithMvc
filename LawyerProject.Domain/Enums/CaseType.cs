using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Domain.Enums
{
    public enum CaseType
    {
        None = 0, //hukuk terimleri gerekli şekilde ingilizceye cevrilecek bunun icin de danışman avukattan yardıma alınacak
        BosanmaDavasi = 1,
        TazminatDavasi = 2,
        KiraDavasi = 3, //Avukata danışılarak buraya gerekli davalar eklenecek
    }
}
