public class WorkControllerTest : IWorkController
{
    private OreMiningController _oreMiningController;

    public WorkControllerTest()
    {
        _oreMiningController = new OreMiningController();
    }

    public QteMiningSetting ProcessOreMining(OreNode oreNode)
    {
        return _oreMiningController.ProcessOreMining(oreNode);
    }
}