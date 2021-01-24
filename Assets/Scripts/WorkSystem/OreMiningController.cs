public class OreMiningController
{
    public QteMiningSetting ProcessOreMining(OreNodeInteractable oreNodeInteractable)
    {
        var numberOfQTE = GetNumberOfQTE(oreNodeInteractable);

        if (numberOfQTE == 0)
        {
            return new QteMiningSetting(0, 0);
        }

        var difficultyOfQTE = GetDifficultyOfQTE(oreNodeInteractable);

        return new QteMiningSetting(numberOfQTE, difficultyOfQTE);
    }


    private int GetDifficultyOfQTE(OreNodeInteractable oreNodeInteractable)
    {
        return 1;
    }

    private int GetNumberOfQTE(OreNodeInteractable oreNodeInteractable)
    {
        return 2;
    }
}