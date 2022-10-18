using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// ѡ�����������ֱ�۵�һ���㷨������˼����ǴӴ����������Ԫ����ѡ����С��һ��Ԫ����Ϊ��Ԫ�أ�֪������Ԫ������Ϊֹ
/// ʱ�临�Ӷȣ�������n-1�֣�����ʱ�临�Ӷ���O(N^2)
/// �ռ临�Ӷ���Ϊ��Ҫһ������ռ䣬������O(1)
/// ѡ�������㷨�ǲ��ȶ������㷨����������ͬԪ�أ��п��ܻ�ı���ͬԪ�ص�˳��
/// ��: 254261->154262->124562->122456�� ����ĵ�һ��2�䵽�˵ڶ���2������
/// </summary>
public class ѡ�������㷨 : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        int[] array = { 4, 5, 3, 1, 2 };
        int minIndex = 0;
        for (int i = 0; i < array.Length - 1; i++)
        {
            minIndex = i;
            for(int j = i + 1; j < array.Length; j++)
            {
                minIndex = array[minIndex] > array[j] ? j : minIndex;
            }

            if(i != minIndex)
            {
                int temp = array[i];
                array[i] = array[minIndex];
                array[minIndex] = temp;
            }

            StringBuilder stringBuilder = new StringBuilder();
            for (int j = 0; j < array.Length; j++)
            {
                stringBuilder.Append(array[j]);
            }
            Debug.Log(stringBuilder.ToString());
        }
    }
}
