public class OreMiningController
{
    public QteMiningSetting ProcessOreMining(OreNode oreNode)
    {
        var numberOfQTE = GetNumberOfQTE(oreNode);

        if (numberOfQTE == 0)
        {
            return new QteMiningSetting(0, 0);
        }

        var difficultyOfQTE = GetDifficultyOfQTE(oreNode);

        return new QteMiningSetting(numberOfQTE, difficultyOfQTE);
    }


    private int GetDifficultyOfQTE(OreNode oreNode)
    {
        return 1;
    }

    private int GetNumberOfQTE(OreNode oreNode)
    {
        return 2;
    }
}