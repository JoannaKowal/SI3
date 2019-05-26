

namespace SI3Backend
{
    public enum FieldState
    {
        Empty,
        White,
        Black
    }
    public class Board
    {
        FieldState[] m_fields = new FieldState[24];

        public Board()
        {
            Clear();
        }

        public void Clear()
        {
            for (var i = 0; i < m_fields.Length; ++i)
            {
                m_fields[i] = FieldState.Empty;
            }
        }
        public FieldState GetFieldState(BoardFieldId id)
        {
            return m_fields[ToIndex(id)];
        }

        public void ChangeFieldState(BoardFieldId id, FieldState newState)
        {
            m_fields[ToIndex(id)] = newState;
        }

        private int ToIndex(BoardFieldId id)
        {
            return id.m_ringId * BoardFieldId.MaxFieldId + id.m_fieldId;
        }

        private BoardFieldId FromIndex(int index)
        {
            return new BoardFieldId(index / BoardFieldId.MaxRingId, index % BoardFieldId.MaxFieldId);
        }
        public bool IsMill(BoardFieldId id)
        {
            FieldState state = this.GetFieldState(id);
            BoardFieldId temp = id;
            if(id.m_fieldId % 2 == 0) // narożniki
            {
                id.m_fieldId++;
                if(this.GetFieldState(id) == state)
                {
                    id.m_fieldId = (id.m_fieldId + 1) % 8;
                    if(this.GetFieldState(id) == state)
                    {
                        return true;
                    }
                }
                id = temp;
                id.m_fieldId = (id.m_fieldId - 1) % 8;
                if (this.GetFieldState(id) == state)
                {
                    id.m_fieldId = (id.m_fieldId - 1) % 8;
                    if (this.GetFieldState(id) == state)
                    {
                        return true;
                    }
                }
                return false;

            }

        }
    }
}
