using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        internal ValueOutOfRangeException(Exception i_InnerException, float i_MaxValue, float i_MinValue) :
            base(string.Format("Value was not in the range of {0} - {1}", i_MinValue, i_MaxValue), i_InnerException)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }
    }
}