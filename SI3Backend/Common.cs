using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SI3Backend
{
    public struct BoardFieldId
    {
        public const int MaxRingId = 3;
        public const int MaxFieldId = 8;

        internal int m_ringId;
        internal int m_fieldId;

        public BoardFieldId(int ring, int id)
        {
            Debug.Assert(ring >= 0 && ring < MaxRingId);
            Debug.Assert(id >= 0 && id < MaxFieldId);

            m_ringId = ring;
            m_fieldId = id;
        }

        public bool IsNeighbour(BoardFieldId otherField)
        {
            if (otherField.m_ringId == m_ringId)
            {
                return otherField.m_fieldId == (m_fieldId + 1) % 8 ||
                    otherField.m_fieldId ==  (m_fieldId - 1) % 8;
            }
            else if (otherField.m_fieldId == m_fieldId && m_fieldId % 2 == 1)
            {
                return otherField.m_ringId == m_ringId + 1 ||
                    otherField.m_ringId == m_ringId - 1;
            }
            return false;
        }
    };
}