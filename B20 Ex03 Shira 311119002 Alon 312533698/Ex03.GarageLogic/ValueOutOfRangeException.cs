using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException :Exception
    {
        float m_MaxValue;
        float m_MinValue;        

        internal ValueOutOfRangeException(float i_MaxValue, float i_MinValue) : base(string.Format("An error occured Value was not in range {0} - {1}",i_MinValue,i_MaxValue))
        {            
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }

        internal ValueOutOfRangeException(Exception i_InnerException ,float i_MaxValue, float i_MinValue) : base(string.Format("An error occured Value was not in range {0} - {1}", i_MinValue, i_MaxValue), i_InnerException)
        {

            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }

    }
}
