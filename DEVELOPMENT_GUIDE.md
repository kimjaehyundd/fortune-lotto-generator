# 🛠️ 개발자 가이드

## 📁 프로젝트 구조 상세 분석

### 메인 클래스들

#### `Form1.cs` - 메인 UI 컨트롤러
```csharp
public partial class Form1 : Form
{
    private FortuneInterpreter interpreter;  // 사주팔자 해석기
    private LottoNumberGenerator generator;  // 번호 생성기
    
    // 주요 이벤트 핸들러
    private void btnGenerate_Click()    // 번호 생성
    private void btnClear_Click()       // 초기화
    private void txtStory_KeyDown()     // 키보드 단축키
}
```

#### `FortuneInterpreter.cs` - 사주팔자 해석 엔진
```csharp
public class FortuneInterpreter
{
    // 오행 매핑 데이터
    private Dictionary<string, string> emotionElements;  // 감정→오행
    private Dictionary<string, string> foodElements;     // 음식→오행
    private Dictionary<string, string> timeElements;     // 시간→오행
    private Dictionary<string, string> actionElements;   // 행동→오행
    
    // 핵심 메서드
    public FortuneInterpretation InterpretStory(string story)
    private void AnalyzeElements()      // 오행 분석
    private void DetermineMainElement() // 주요 운세 결정
    private void GenerateExplanation()  // 해석문 생성
}
```

#### `LottoNumberGenerator.cs` - 번호 생성 알고리즘
```csharp
public class LottoNumberGenerator
{
    // 오행별 번호 범위
    private Dictionary<string, int[]> elementNumbers;
    
    // 핵심 메서드
    public int[] GenerateNumbers()           // 메인 생성 로직
    private int[] GetNumbersFromElement()    // 오행별 번호 추출
    private int[] AdjustNumbersNumerologically() // 수비학적 조정
}
```

## 🧠 알고리즘 상세 분석

### 1. 텍스트 분석 알고리즘

```csharp
// 키워드 매칭 방식
foreach (var emotion in emotionElements)
{
    if (story.Contains(emotion.Key))
    {
        interpretation.ElementValues[emotion.Value] += 2;
    }
}
```

**가중치 시스템**
- 음식 키워드: +3점 (가장 중요)
- 감정/행동 키워드: +2점
- 시간 키워드: +1점
- 텍스트 길이: +(길이%5)점

### 2. 오행 매핑 시스템

**금(金) - 의리와 청렴**
- 감정: 슬픔, 그리움
- 음식: 우유, 달걀, 아이스크림
- 시간: 어제, 과거
- 행동: 쇼핑, 돈 관련

**목(木) - 성장과 발전**
- 감정: 고민, 걱정, 스트레스
- 음식: 야채, 샐러드, 상추
- 시간: 아침, 새벽, 오전
- 행동: 산책, 운동, 공부

**물(水) - 지혜와 유연성**
- 감정: 피곤, 지침, 느긋함
- 음식: 라면, 국물, 물
- 시간: 밤, 야식
- 행동: 잠, 휴식

**불(火) - 열정과 활력**
- 감정: 기쁨, 행복, 화남
- 음식: 고기, 소시지, 치킨
- 시간: 점심, 낮, 오후
- 행동: 일, 공부, 활동

**토(土) - 안정과 신뢰**
- 감정: 배고픔, 만족
- 음식: 빵, 밥, 감자
- 시간: 저녁, 저녁식사
- 행동: 먹기, 식사

### 3. 번호 생성 알고리즘

```csharp
// 1단계: 주요 운세에서 2개 번호
var mainElementNumbers = GetNumbersFromElement(interpretation.MainElement, 2);

// 2단계: 강한 오행들에서 추가 번호
var strongElements = interpretation.ElementValues
    .Where(x => x.Value >= 2)
    .OrderByDescending(x => x.Value)
    .Take(3);

// 3단계: 수비학적 조정 (합: 120-180)
var result = AdjustNumbersNumerologically(numbers);
```

**오행별 번호 범위**
```csharp
elementNumbers = new Dictionary<string, int[]>
{
    {"금", new int[] {1, 6, 11, 16, 21, 26, 31, 36, 41}},
    {"목", new int[] {3, 8, 13, 18, 23, 28, 33, 38, 43}},
    {"물", new int[] {2, 7, 12, 17, 22, 27, 32, 37, 42}},
    {"불", new int[] {4, 9, 14, 19, 24, 29, 34, 39, 44}},
    {"토", new int[] {5, 10, 15, 20, 25, 30, 35, 40, 45}}
};
```

## 🎨 UI 디자인 가이드

### 색상 팔레트
- **주요 색상**: DarkBlue (#000080)
- **보조 색상**: DarkGreen (#006400)
- **강조 색상**: Red (#FF0000)
- **배경 색상**: WhiteSmoke (#F5F5F5)
- **입력 배경**: LightYellow (#FFFFE0)

### 폰트 설정
```csharp
// 제목 폰트
new Font("맑은 고딕", 18F, FontStyle.Bold)

// 본문 폰트
new Font("맑은 고딕", 10F, FontStyle.Regular)

// 번호 표시 폰트
new Font("맑은 고딕", 14F, FontStyle.Bold)
```

### 레이아웃 구조
```
┌─────────────────────────────┐
│ 제목 (행운의 로또번호 생성기)      │
├─────────────────────────────┤
│ 안내문구                     │
│ ┌─────────────────────────┐   │
│ │ 이야기 입력창 (멀티라인)    │   │
│ └─────────────────────────┘   │
│ [운명해석] [초기화]           │
├─────────────────────────────┤
│ 사주팔자 해석:               │
│ ┌─────────────────────────┐   │
│ │ 해석 결과 (읽기전용)      │   │
│ └─────────────────────────┘   │
├─────────────────────────────┤
│ 운명의 로또번호: [1][2][3]... │
└─────────────────────────────┘
```

## 🔧 커스터마이징 가이드

### 새로운 키워드 추가

**FortuneInterpreter.cs의 InitializeMappings()에서:**
```csharp
// 새로운 감정 키워드 추가
emotionElements.Add("신나", "불");
emotionElements.Add("우울", "금");

// 새로운 음식 키워드 추가
foodElements.Add("피자", "불");
foodElements.Add("커피", "목");
```

### 해석 메시지 추가

**GetFortuneInterpretation() 메서드에서:**
```csharp
var interpretations = new Dictionary<string, string[]>
{
    {"금", new string[] {
        "기존 메시지들...",
        "새로운 해석 메시지 추가"
    }}
};
```

### 번호 범위 수정

**LottoNumberGenerator.cs에서:**
```csharp
// 오행별 번호 범위 변경
elementNumbers["금"] = new int[] {1, 7, 13, 19, 25, 31, 37, 43};
```

## 🐛 디버깅 가이드

### 주요 디버깅 포인트

1. **텍스트 분석 확인**
```csharp
// FortuneInterpreter.AnalyzeElements()에 브레이크포인트
foreach (var emotion in emotionElements)
{
    if (story.Contains(emotion.Key))  // ← 여기서 매칭 확인
    {
        interpretation.ElementValues[emotion.Value] += 2;
    }
}
```

2. **오행 점수 확인**
```csharp
// DetermineMainElement()에서 최종 점수 확인
var maxElement = interpretation.ElementValues
    .OrderByDescending(x => x.Value)  // ← 여기서 점수 순서 확인
    .First();
```

3. **번호 생성 과정 확인**
```csharp
// GenerateNumbers()에서 각 단계별 번호 확인
var mainNumbers = GetNumbersFromElement(interpretation.MainElement, 2);
// ← mainNumbers 내용 확인
```

### 일반적인 문제 해결

**문제: 같은 입력에 다른 결과**
- 원인: Random 클래스 사용
- 해결: 시드값 고정 또는 해시 기반 랜덤 사용

**문제: 키워드 매칭 안됨**
- 원인: 대소문자, 띄어쓰기 차이
- 해결: ToLower(), Trim() 적용

**문제: UI 깨짐**
- 원인: DPI 설정, 폰트 미설치
- 해결: AutoScaleMode 설정, 기본 폰트 사용

## 🚀 성능 최적화

### 메모리 최적화
```csharp
// StringBuilder 사용으로 문자열 연산 최적화
var explanation = new StringBuilder();
explanation.AppendLine("내용...");
```

### 알고리즘 최적화
```csharp
// Dictionary 사용으로 O(1) 검색
if (emotionElements.ContainsKey(keyword))
{
    // 빠른 키워드 매칭
}
```

## 📦 배포 가이드

### Release 빌드
1. **솔루션 구성** → **Release** 선택
2. **빌드** → **솔루션 다시 빌드**
3. `bin/Release/` 폴더에서 실행 파일 확인

### 설치 프로그램 만들기
1. Visual Studio Installer Projects 확장 설치
2. 새 Setup Project 추가
3. Primary Output 추가
4. 빌드하여 MSI 생성

---

**해피 코딩! 🎯**