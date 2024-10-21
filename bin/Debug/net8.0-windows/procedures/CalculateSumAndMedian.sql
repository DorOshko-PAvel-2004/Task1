CREATE PROCEDURE CalculateSumAndMedian
AS
BEGIN
    -- Переменная для хранения суммы целых чисел
    DECLARE @SumOfIntegers BIGINT;

    -- Переменная для хранения медианы дробных чисел
    DECLARE @MedianOfDecimals DECIMAL(10,8);

    -- Вычисление суммы всех целых чисел
    SELECT @SumOfIntegers = SUM(cast(TaskInt as Bigint))
    FROM Task1;

    -- Вычисление медианы дробных чисел
    WITH SortedDecimals AS
    (
        SELECT 
            TaskFloat,
            ROW_NUMBER() OVER (ORDER BY TaskFloat) AS RowAsc,
            ROW_NUMBER() OVER (ORDER BY TaskFloat DESC) AS RowDesc
        FROM Task1
        WHERE TaskFloat IS NOT NULL
    )
    SELECT @MedianOfDecimals = AVG(TaskFloat)
    FROM SortedDecimals
    WHERE RowAsc = RowDesc -- Нечетное количество строк
       OR RowAsc + 1 = RowDesc; -- Четное количество строк

    -- Вывод результата
    SELECT 
        @SumOfIntegers AS SumOfIntegers,
        @MedianOfDecimals AS MedianOfDecimals;
END;

exec CalculateSumAndMedian


