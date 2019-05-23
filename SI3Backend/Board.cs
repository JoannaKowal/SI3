

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
    }
}
