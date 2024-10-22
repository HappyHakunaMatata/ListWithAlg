using System;
namespace Enumerator
{
	public delegate T GetElementByIndexDelegate<out T>(int index);
    public delegate bool SetElementByIndexDelegate<in T>(int index, T Data);
}

