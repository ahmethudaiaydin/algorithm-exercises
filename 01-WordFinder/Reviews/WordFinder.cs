namespace puzzle
{
    public class WordFinder
    {
        private string[] Vector { get; set; }
        public WordFinder(string[] vector)
        {
            // REVIEW: Ya vector null ise?
            Vector = vector;
        }

        // REFACTOR: Algoritma kurarken bu kadar tekrar islem ve kod bize bir sinyal vermiyor mu?
        private bool SearchNeighbourhood(int positionX, int positionY, string key, int keyIndex)
        {
            if (key.Length == keyIndex)
            {
                return true;
            }

            //North-west
            if (positionX > 0 && positionY > 0)
            {
                var northWestChar = Vector[positionY - 1][positionX - 1];
                if (northWestChar == key[keyIndex])
                {
                    return SearchNeighbourhood(positionX - 1, positionY - 1, key, keyIndex + 1);
                }

            }

            //North
            if (positionY > 0)
            {
                var northChar = Vector[positionY - 1][positionX];
                if (northChar == key[keyIndex])
                {
                    return SearchNeighbourhood(positionX, positionY - 1, key, keyIndex + 1);
                }

            }

            //North-East
            if (positionY > 0 && positionX < Vector[positionY].Length-1)
            {
                var northEastChar = Vector[positionY - 1][positionX + 1];
                if (northEastChar == key[keyIndex])
                {
                    return SearchNeighbourhood(positionX + 1, positionY - 1, key, keyIndex + 1);
                }
            }

            //East
            if (positionX < Vector[positionY].Length-1)
            {
                var eastChar = Vector[positionY][positionX + 1];
                if (eastChar == key[keyIndex])
                {
                    return SearchNeighbourhood(positionX + 1, positionY, key, keyIndex + 1);
                }
            }

            //South-East
            if (positionY < Vector.Length && positionX < Vector[positionY].Length-1)
            {
                var southEastChar = Vector[positionY + 1][positionX + 1];
                if (southEastChar == key[keyIndex])
                {
                    return SearchNeighbourhood(positionX + 1, positionY + 1, key, keyIndex + 1);
                }
            }

            //South

            if (positionY < Vector.Length-1)
            {
                var southChar = Vector[positionY + 1][positionX];
                if (southChar == key[keyIndex])
                {
                    return SearchNeighbourhood(positionX, positionY + 1, key, keyIndex + 1);
                }
            }

            //South-West
            if (positionY < Vector.Length-1 && positionX > 0)
            {
                var southEastChar = Vector[positionY + 1][positionX - 1];
                if (southEastChar == key[keyIndex])
                {
                    return SearchNeighbourhood(positionX - 1, positionY + 1, key, keyIndex + 1);
                }
            }

            //West 
            if (positionX > 0)
            {
                var westChar = Vector[positionY][positionX - 1];
                if (westChar == key[keyIndex])
                {
                    return SearchNeighbourhood(positionX - 1, positionY, key, keyIndex + 1);
                }
            }
            return false;
        }

        private bool IsFirstChar(string word, char controlChar)
        {
            return controlChar == word[0];
        }

        // REVIEW: Ya word null ise?
        public bool IsWordExists(string word)
        {
            bool isWordFound = false;

            // REVIEW: Ya word bos degerdeyse?

            // REVIEW: Property nedir, loop icinde propery uzerinden erismek iyi midir?
            // Vector.Length
            for (int positionY = 0; positionY < Vector.Length; positionY++)
            {
                // REVIEW: Property nedir, loop icinde propery uzerinden erismek iyi midir?
                // Vector[positionY].Length
                for (int positionX = 0; positionX < Vector[positionY].Length; positionX++)
                {
                    if (!IsFirstChar(word, Vector[positionY][positionX]))
                        continue;

                    //We already found first char. Therefore we send 1 to start scanning with second char.
                    if (SearchNeighbourhood(positionX, positionY, word, 1))
                    {
                        return true;
                    }
                } 
            }

            return isWordFound;
        }
    }
}
