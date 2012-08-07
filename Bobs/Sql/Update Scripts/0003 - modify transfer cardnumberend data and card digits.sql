/*
UPDATE Transfer SET CardDigits = Len(CardNumberEnd)
UPDATE Transfer SET CardNumberEnd = SubString(CardNumberEnd, Len(CardNumberEnd) - 5, Len(CardNumberEnd)) FROM Transfer WHERE Len(CardNumberEnd) > 6 AND SubString(CardNumberEnd, 1, 1) = '*' 
*/
