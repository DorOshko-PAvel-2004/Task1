CREATE PROCEDURE CalculateSumAndMedian
AS
BEGIN
    -- ���������� ��� �������� ����� ����� �����
    DECLARE @SumOfIntegers BIGINT;

    -- ���������� ��� �������� ������� ������� �����
    DECLARE @MedianOfDecimals DECIMAL(10,8);

    -- ���������� ����� ���� ����� �����
    SELECT @SumOfIntegers = SUM(cast(TaskInt as Bigint))
    FROM Task1;

    -- ���������� ������� ������� �����
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
    WHERE RowAsc = RowDesc -- �������� ���������� �����
       OR RowAsc + 1 = RowDesc; -- ������ ���������� �����

    -- ����� ����������
    SELECT 
        @SumOfIntegers AS SumOfIntegers,
        @MedianOfDecimals AS MedianOfDecimals;
END;

exec CalculateSumAndMedian


