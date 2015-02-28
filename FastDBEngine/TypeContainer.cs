using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal class TypeContainer<TAsm, T>
{
    private readonly TAsm asm;
    private readonly T t;
    public TypeContainer(TAsm asm, T t)
    {
        this.asm = asm;
        this.t = t;
    }


    public TAsm Tasm
    {
        get
        {
            return asm;
        }
    }

    public T Ttype
    {
        get
        {
            return t;
        }
    }

}
