using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

public class craft_controller
{
    private CraftController _craftController;

    [SetUp]
    public void Init()
    {
        var subIHaveInventories = Substitute.For<IHaveInventories>();
        _craftController=new CraftController(subIHaveInventories);
    }
    
    
}
