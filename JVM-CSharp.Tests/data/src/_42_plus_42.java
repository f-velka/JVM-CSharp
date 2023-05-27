class _42_plus_42
{
    private int test;

    _42_plus_42()
    {
        test = 42;
    }

    public int Do()
    {
        return test + test;
    }

    public static void main(String[] args)
    {
        _42_plus_42 addTest = new _42_plus_42();
        System.out.println(addTest.Do());
    }
}