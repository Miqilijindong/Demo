using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// ����������һ�ּ�ֱ�۵������㷨�����Ĺ���ԭ����ͨ�������������У�����δ�������ݣ����������дӺ���ǰɨ�裬�ҵ���Ӧλ�ò�����
/// ʱ�临�Ӷ���O(N^2)
/// �ռ临�Ӷ���O(1)
/// ���ȶ��������������в��뵽�������������ı���ͬԪ�ص�ǰ��˳����һ���ȶ������㷨
/// </summary>
public class �������� : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] array = { 4, 5, 3, 1, 2 };
        int insertValue, j;
        for (int i = 1; i < array.Length; i++)
        {
            insertValue = array[i];
            j = i - 1;
            for (; j >= 0 && insertValue < array[j]; j--)
            {
                array[j + 1] = array[j];
            }
            array[j + 1] = insertValue;


            StringBuilder stringBuilder = new StringBuilder();
            for (int x = 0; x < array.Length; x++)
            {
                stringBuilder.Append(array[x]);
            }
            Debug.Log(stringBuilder.ToString());
        }
    }
}
