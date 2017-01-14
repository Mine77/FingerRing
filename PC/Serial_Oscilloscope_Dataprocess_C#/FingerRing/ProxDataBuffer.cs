using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerRing
{
    class ProxDataBuffer
    {
        private double[] Buffer;
        private int Pointer;
        private int Length;
        private bool IsPointerMarked;

        enum PointerMarkerType
        {
            M1 = 1,
            M2 = 2,
            M3 = 3,
            M4 = 4,
            M5 = 5,
            M6 = 6,
        };

        public ProxDataBuffer(int len)
        {
            Length = len;
            Buffer = new double[Length];
            Buffer.Initialize();
            Pointer = Length - 1;
            IsPointerMarked = false;
        }

        public void AddData(double _data)
        {
            MovePointer(1);
            Buffer[Pointer] = _data;
        }

        public void MarkPointer(PointerMarkerType mtype)
        {

        }


        //return the data from relevent position:
        //pos   -3 -2 -1  0  1  2  3
        //data   *  *  *  *  *  *  *
        //index  0  1  2  3  4  5  6
        public double ReturnData(int _pos)
        {
            int tempPtr = Pointer;
            if (_pos < 0)
            {
                _pos += Length;
            }
            tempPtr = (tempPtr + _pos) % Length;
            return Buffer[tempPtr];
        }
        private void MovePointer(int _times)
        {
            if (_times < 0)
            {
                _times += Length;
            }
            Pointer = (Pointer + _times) % Length;
        }

    }
}
