using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwipeUI : MonoBehaviour
{
	[SerializeField]
	private	Scrollbar	scrollBar;					// Scrollbar�� ��ġ�� �������� ���� ������ �˻�
	[SerializeField]
	private	Transform[]	circleContents;				// ���� �������� ��Ÿ���� �� Image UI���� Transform
	[SerializeField]
	private	float		swipeTime = 0.2f;			// �������� Swipe �Ǵ� �ð�
	[SerializeField]
	private	float		swipeDistance = 50.0f;		// �������� Swipe�Ǳ� ���� �������� �ϴ� �ּ� �Ÿ�

	private	float[]		scrollPageValues;			// �� �������� ��ġ �� [0.0 - 1.0]
	private	float		valueDistance = 0;			// �� ������ ������ �Ÿ�
	private	int			currentPage = 0;			// ���� ������
	private	int			maxPage = 0;				// �ִ� ������
	private	float		startTouchX;				// ��ġ ���� ��ġ
	private	float		endTouchX;					// ��ġ ���� ��ġ
	private	bool		isSwipeMode = false;		// ���� Swipe�� �ǰ� �ִ��� üũ
	private	float		circleContentScale = 1f;	// ���� �������� �� ũ��(����)

	private void Awake()
	{
		// ��ũ�� �Ǵ� �������� �� value ���� �����ϴ� �迭 �޸� �Ҵ�
		scrollPageValues = new float[transform.childCount];

		// ��ũ�� �Ǵ� ������ ������ �Ÿ�
		valueDistance = 1f / (scrollPageValues.Length - 1f);

		// ��ũ�� �Ǵ� �������� �� value ��ġ ���� [0 <= value <= 1]
		for (int i = 0; i < scrollPageValues.Length; ++ i )
		{
			scrollPageValues[i] = valueDistance * i;
		}

		// �ִ� �������� ��
		maxPage = transform.childCount;
	}

	private void Start()
	{
		// ���� ������ �� 0�� �������� �� �� �ֵ��� ����
		SetScrollBarValue(0);
	}

	public void SetScrollBarValue(int index)
	{
		currentPage		= index;
		scrollBar.value	= scrollPageValues[index];
	}

	private void Update()
	{
		//UpdateInput();

		// �Ʒ��� ��ġ�� ������ ��ư ����
		UpdateCircleContent();
	}

	private void UpdateInput()
	{
		// ���� Swipe�� �������̸� ��ġ �Ұ�
		if ( isSwipeMode == true ) return;

		#if UNITY_EDITOR
		// ���콺 ���� ��ư�� ������ �� 1ȸ
		if ( Input.GetMouseButtonDown(0) )
		{
			// ��ġ ���� ���� (Swipe ���� ����)
			startTouchX = Input.mousePosition.x;
		}
		else if ( Input.GetMouseButtonUp(0) )
		{
			// ��ġ ���� ���� (Swipe ���� ����)
			endTouchX = Input.mousePosition.x;

			UpdateSwipe();
		}
		#endif

		#if UNITY_ANDROID
		if ( Input.touchCount == 1 )
		{
			Touch touch = Input.GetTouch(0);

			if ( touch.phase == TouchPhase.Began )
			{
				// ��ġ ���� ���� (Swipe ���� ����)
				startTouchX = touch.position.x;
			}
			else if ( touch.phase == TouchPhase.Ended )
			{
				// ��ġ ���� ���� (Swipe ���� ����)
				endTouchX = touch.position.x;

				UpdateSwipe();
			}
		}
		#endif
	}

	private void UpdateSwipe()
	{
		/*
		// �ʹ� ���� �Ÿ��� �������� ���� Swipe X
		if ( Mathf.Abs(startTouchX-endTouchX) < swipeDistance )
		{
			// ���� �������� Swipe�ؼ� ���ư���
			StartCoroutine(OnSwipeOneStep(currentPage));
			return;
		}
		*/
		// Swipe ����
		bool isLeft = startTouchX < endTouchX ? true : false;

		// �̵� ������ ������ ��
		if ( isLeft == true )
		{
			// ���� �������� ���� ���̸� ����
			if ( currentPage == 0 ) return;

			// �������� �̵��� ���� ���� �������� 1 ����
			currentPage --;
		}
		// �̵� ������ �������� ��
		else
		{
			// ���� �������� ������ ���̸� ����
			if ( currentPage == maxPage - 1 ) return;

			// ���������� �̵��� ���� ���� �������� 1 ����
			currentPage ++;
		}

		// currentIndex��° �������� Swipe�ؼ� �̵�
		StartCoroutine(OnSwipeOneStep(currentPage));
	}

	public void MoveToPageIndex(int index) {
		currentPage = index;
		StartCoroutine(OnSwipeOneStep(currentPage));
	}


	/// <summary>
	/// �������� �� �� ������ �ѱ�� Swipe ȿ�� ���
	/// </summary>
	private IEnumerator OnSwipeOneStep(int index)
	{
		float start		= scrollBar.value;
		float current	= 0;
		float percent	= 0;

		isSwipeMode = true;

		while ( percent < 1 )
		{
			current += Time.deltaTime;
			percent = current / swipeTime;

			scrollBar.value = Mathf.Lerp(start, scrollPageValues[index], percent);

			yield return null;
		}

		isSwipeMode = false;
	}

	private void UpdateCircleContent()
	{
		// �Ʒ��� ��ġ�� ������ ��ư ũ��, ���� ���� (���� �ӹ��� �ִ� �������� ��ư�� ����)
		for ( int i = 0; i < scrollPageValues.Length; ++ i )
		{
			circleContents[i].localScale					= Vector2.one;
			//circleContents[i].GetComponent<Image>().color	= Color.white;

			// �������� ������ �Ѿ�� ���� ������ ���� �ٲٵ���
			if ( scrollBar.value < scrollPageValues[i] + (valueDistance / 2) && scrollBar.value > scrollPageValues[i] - (valueDistance / 2) )
			{
				circleContents[i].localScale					= Vector2.one * circleContentScale;
				//circleContents[i].GetComponent<Image>().color	= Color.black;
			}
		}
	}
}



/*using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwipeUI : MonoBehaviour
{
	[SerializeField]
	private	Scrollbar	scrollBar;				// Scrollbar�� ��ġ�� �������� ���� ������ �˻�
	
	[SerializeField]
	private	float		swipeTime = 0.2f;		// �������� Swipe �Ǵ� �ð�
	[SerializeField]
	private	float		swipeDistance = 50.0f;	// �������� Swipe�Ǳ� ���� �������� �ϴ� �ּ� �Ÿ�

	private	float[]		arrayScrollValue;		// �� �������� ��ġ �� [0.0 - 1.0]
	private	float		valueDistance = 0;		// �� ������ ������ �Ÿ�
	private	int			currentPage = 0;		// ���� ������
	private	int			maxPage = 0;			// �ִ� ������
	private	float		startTouchX;			// ��ġ ���� ��ġ
	private	float		endTouchX;				// ��ġ ���� ��ġ
	private	bool		isSwipeMode = false;	// ���� Swipe�� �ǰ� �ִ��� üũ

	private	float		circleContentScale = 1.6f;	// ���� �������� �� ũ��(����)

	private void Awake()
	{
		// ��ũ�� �Ǵ� �������� �� value ���� �����ϴ� �迭 �޸� �Ҵ�
		arrayScrollValue = new float[transform.childCount];

		// ��ũ�� �Ǵ� ������ ������ �Ÿ�
		valueDistance = 1f / (arrayScrollValue.Length - 1f);

		// ��ũ�� �Ǵ� �������� �� value ��ġ ���� [0 <= value <= 1]
		for (int i = 0; i < arrayScrollValue.Length; ++ i )
		{
			arrayScrollValue[i] = valueDistance * i;
		}

		// �ִ� �������� ��
		maxPage = transform.childCount;
	}

	private void Start()
	{
		// ���� ������ �� 0�� �������� �� �� �ֵ��� ����
		SetScrollBarValue(0);
	}

	public void SetScrollBarValue(int index)
	{
		currentPage		= index;
		scrollBar.value	= arrayScrollValue[index];
	}

	private void Update()
	{
		// �Ʒ��� ��ġ�� ������ ��ư ����
		UpdateCircleContent();

		// ���� Swipe�� �������̸� ��ġ �Ұ�
		if ( isSwipeMode == true ) return;

		#if UNITY_EDITOR
		// ���콺 ���� ��ư�� ������ �� 1ȸ
		if ( Input.GetMouseButtonDown(0) )
		{
			// ��ġ ���� ���� (Swipe ���� ����)
			startTouchX = Input.mousePosition.x;
		}
		else if ( Input.GetMouseButtonUp(0) )
		{
			// ��ġ ���� ���� (Swipe ���� ����)
			endTouchX = Input.mousePosition.x;

			UpdateSwipe();
		}
		#endif

		#if UNITY_ANDROID
		if ( Input.touchCount == 1 )
		{
			Touch touch = Input.GetTouch(0);

			if ( touch.phase == TouchPhase.Began )
			{
				// ��ġ ���� ���� (Swipe ���� ����)
				startTouchX = touch.position.x;
			}
			else if ( touch.phase == TouchPhase.Ended )
			{
				// ��ġ ���� ���� (Swipe ���� ����)
				endTouchX = touch.position.x;

				UpdateSwipe();
			}
		}
		#endif
	}

	private void UpdateSwipe()
	{
		// �ʹ� ���� �Ÿ��� �������� ���� Swipe X
		if ( Mathf.Abs(startTouchX-endTouchX) < swipeDistance )
		{
			// ���� �������� Swipe�ؼ� ���ư���
			StartCoroutine(OnSwipeOneStep(currentPage));
			return;
		}

		// Swipe ����
		bool isLeft = startTouchX < endTouchX ? true : false;

		// �̵� ������ ������ ��
		if ( isLeft == true )
		{
			// ���� �������� ���� ���̸� ����
			if ( currentPage == 0 )
			{
				return;
			}

			currentPage --;
		}
		// �̵� ������ �������� ��
		else
		{
			// ���� �������� ������ ���̸� ����
			if ( currentPage == maxPage - 1 )
			{
				return;
			}

			currentPage ++;
		}

		// currentIndex��° �������� Swipe�ؼ� �̵�
		StartCoroutine(OnSwipeOneStep(currentPage));
	}

	/// <summary>
	/// �������� �� �� ������ �ѱ�� Swipe ȿ�� ���
	/// </summary>
	private IEnumerator OnSwipeOneStep(int index)
	{
		float start		= scrollBar.value;
		float current	= 0;
		float percent	= 0;

		isSwipeMode = true;

		while ( percent < 1 )
		{
			current += Time.deltaTime;
			percent = current / swipeTime;

			scrollBar.value = Mathf.Lerp(start, arrayScrollValue[index], percent);

			yield return null;
		}

		isSwipeMode = false;
	}

	private void UpdateCircleContent()
	{
		// �Ʒ��� ��ġ�� ������ ��ư ũ��, ���� ���� (���� �ӹ��� �ִ� �������� ��ư�� ����)
		for ( int i = 0; i < arrayScrollValue.Length; ++ i )
		{
			circleContents[i].localScale					= Vector2.one;
			circleContents[i].GetComponent<Image>().color	= Color.white;

			// �������� ������ �Ѿ�� ���� ������ ���� �ٲٵ���
			if ( scrollBar.value < arrayScrollValue[i] + (valueDistance / 2) && scrollBar.value > arrayScrollValue[i] - (valueDistance / 2) )
			{
				circleContents[i].localScale					= Vector2.one * circleContentScale;
				circleContents[i].GetComponent<Image>().color	= Color.black;
			}
		}
	}
}

*/