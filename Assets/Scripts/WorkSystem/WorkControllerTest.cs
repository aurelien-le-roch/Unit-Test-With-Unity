public class WorkControllerTest : IWorkController
{
    private OreMiningController _oreMiningController;

    public WorkControllerTest()
    {
        _oreMiningController = new OreMiningController();
    }

    public QteMiningSetting ProcessOreMining(OreNodeInteractable oreNodeInteractable)
    {
        return _oreMiningController.ProcessOreMining(oreNodeInteractable);
    }
}