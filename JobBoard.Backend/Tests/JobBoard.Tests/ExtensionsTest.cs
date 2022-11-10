using JobBoard.Application.Common.Extensions;

namespace JobBoard.Tests;

public class ExtensionsTest
{
    class some
    {
        public int a;
        public int b;
    }

    static IQueryable<some> coll1 = new List<some>
        {
            new some(){a = 1, b = 2 },
            new some(){a = 2, b = 4 },
            new some(){a = 6, b = 32 },
            new some(){a = 7, b = 8 },
        }.AsQueryable();

    static IQueryable<some> coll2 = new List<some>
        {
            new some(){a = 1, b = 2 },
            new some(){a = 2, b = 4 },
            new some(){a = 7, b = 8 },
            new some(){a = 6, b = 32 },
        }.AsQueryable();

    static IQueryable<some> coll3 = new List<some>
        {
            new some(){a = 7, b = 8 },
            new some(){a = 6, b = 32 },
            new some(){a = 2, b = 4 },
            new some(){a = 1, b = 2 },
        }.AsQueryable();

    static IQueryable<some> coll4 = new List<some>
        {
            new some(){a = 6, b = 32 },
            new some(){a = 7, b = 8 },
            new some(){a = 2, b = 4 },
            new some(){a = 1, b = 2 },
        }.AsQueryable();

    [Fact]
    public void Test1()
    {
        var sort = coll1.OrderBy(x => x.a, true);
        Assert.Equal(coll1, sort);
    }

    [Fact]
    public void Test2()
    {
        var sort = coll1.OrderBy(x => x.b, true);
        Assert.Equal(coll2, sort);
    }
    
    [Fact]
    public void Test3()
    {
        var sort = coll1.OrderBy(x => x.a, false);
        Assert.Equal(coll3, sort);
    }
    
    [Fact]
    public void Test4()
    {
        var sort = coll1.OrderBy(x => x.b, false);
        Assert.Equal(coll4, sort);
    }

}