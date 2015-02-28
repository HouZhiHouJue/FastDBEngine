using System;
using System.Collections.Generic;

internal sealed class OutParametersInfo
{
    private List<string> OutParameterList;
    public object objClass;

    public List<string> GetOutParameterList()
    {
        return this.OutParameterList;
    }

    public void AddOutParameter(string parameter)
    {
        if (this.OutParameterList == null)
        {
            this.OutParameterList = new List<string>(10);
        }
        this.OutParameterList.Add(parameter);
    }
}

