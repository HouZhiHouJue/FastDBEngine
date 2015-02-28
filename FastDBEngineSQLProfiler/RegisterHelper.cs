using Microsoft.Win32;
using System;
using System.Collections;
public static class RegisterHelper
{
	public static Exception RegisterHashtable(RegistryKey registrykey, string keyName, Hashtable hashtable)
	{
		RegistryKey registryKey = null;
		Exception result;
		try
		{
			registryKey = registrykey.OpenSubKey(keyName, false);
			if (registryKey != null)
			{
				string[] array = new string[hashtable.Count];
				hashtable.Keys.CopyTo(array, 0);
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					hashtable[text] = registryKey.GetValue(text, hashtable[text]);
				}
			}
		}
		catch (Exception ex)
		{
			result = ex;
			return result;
		}
		finally
		{
			if (registryKey != null)
			{
				registryKey.Close();
			}
		}
		result = null;
		return result;
	}
	public static Exception SetRegistryKeyValue(RegistryKey registrykey, string keyName, Hashtable hashtable)
	{
		RegistryKey registryKey = null;
		Exception result;
		try
		{
			registryKey = registrykey.OpenSubKey(keyName, true);
			if (registryKey == null)
			{
				registryKey = Registry.CurrentUser.CreateSubKey(keyName);
			}
			if (registryKey != null)
			{
				foreach (DictionaryEntry dictionaryEntry in hashtable)
				{
					registryKey.SetValue(dictionaryEntry.Key.ToString(), dictionaryEntry.Value);
				}
			}
		}
		catch (Exception ex)
		{
			result = ex;
			return result;
		}
		finally
		{
			if (registryKey != null)
			{
				registryKey.Close();
			}
		}
		result = null;
		return result;
	}
	public static Exception RegisterHashtable(string keyName, Hashtable hashtable)
	{
		return RegisterHelper.RegisterHashtable(Registry.CurrentUser, keyName, hashtable);
	}
	public static Exception SetRegistryKeyValue(string keyName, Hashtable hashtable)
	{
		return RegisterHelper.SetRegistryKeyValue(Registry.CurrentUser, keyName, hashtable);
	}
}
