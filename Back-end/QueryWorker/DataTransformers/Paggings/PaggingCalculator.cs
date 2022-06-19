using QueryWorker.Args;
using System;

namespace QueryWorker.DataTransformers.Paggings;
public static class PaggingCalculator
{
    public static int CalculatePageCount(PaggingArgs pagging, int dataCount)
    {
        return (int)Math.Ceiling(dataCount / (double)pagging.PageSize);
    }

    public static int CalculateCurrentPageDataCount(PaggingArgs pagging, int dataCount)
    {
        var pageNumberDataCount = pagging.PageNumber * pagging.PageSize;

        return dataCount > pageNumberDataCount
            ? pagging.PageSize
            : pageNumberDataCount - dataCount;
    }

    public static bool HasNextPage(PaggingArgs pagging, int dataCount)
    {
        var pageNumberDataCount = pagging.PageNumber * pagging.PageSize;

        return dataCount > pageNumberDataCount;
    }

    public static bool HasPreviousPage(PaggingArgs pagging, int dataCount)
    {
        var pageNumberDataCount = pagging.PageNumber * pagging.PageSize;

        return pageNumberDataCount > pagging.PageSize;
    }
}
