# Unity-RestSharp-Example

이 프로젝트는 Unity 환경에서 RestSharp 라이브러리를 활용하는 간단한 예제입니다. RestSharp은 .NET 애플리케이션에서 HTTP 요청을 간편하게 처리할 수 있는 강력하고 유연한 라이브러리로, Unity에서도 유용하게 활용할 수 있습니다.

이 문서를 통해 RestSharp을 Unity에서 사용하기 위해 어떤 과정을 거쳐야 하는지 이해할 수 있습니다.

---

## 목차

- [Unity-RestSharp-Example](#unity-restsharp-example)
  - [목차](#목차)
  - [개요](#개요)
  - [사전 준비 사항](#사전-준비-사항)
    - [NuGetForUnity 설치](#nugetforunity-설치)
    - [RestSharp 추가](#restsharp-추가)
  - [설치 방법](#설치-방법)
    - [NuGetForUnity 설정](#nugetforunity-설정)
  - [예제 코드](#예제-코드)
  - [추가 정보 및 팁](#추가-정보-및-팁)
    - [공식 문서 참고](#공식-문서-참고)
    - [Unity 환경에서의 주의사항](#unity-환경에서의-주의사항)
    - [빌드 시 고려사항](#빌드-시-고려사항)

---

## 개요

Unity에서 RestSharp을 사용하면 HTTP/HTTPS 통신을 보다 간편하게 처리할 수 있습니다. HttpWebRequest 혹은 UnityWebRequest 등의 기본 API를 직접 사용하는 것보다 높은 수준의 추상화를 제공하기 때문에, 직관적인 코드를 작성할 수 있습니다.

그러나 Unity 환경에서 .NET의 NuGet 패키지를 바로 가져와 사용하기 위해서는 [NuGetForUnity](https://github.com/GlitchEnzo/NuGetForUnity) 플러그인 설치가 필요합니다.

---

## 사전 준비 사항

### NuGetForUnity 설치

Unity 엔진에서 NuGet 패키지를 관리하기 위해서는 NuGetForUnity를 반드시 설치해야 합니다.

> [여기에 NuGetForUnity 설치 화면 이미지를 삽입할 수 있습니다]

NuGetForUnity 설치 및 사용에 대한 자세한 내용은 [공식 GitHub 저장소](https://github.com/GlitchEnzo/NuGetForUnity)를 참고하시기 바랍니다.

### RestSharp 추가

NuGetForUnity가 정상적으로 설치된 후, NuGetForUnity를 통해 RestSharp 라이브러리를 쉽게 가져올 수 있습니다.

---

## 설치 방법

### NuGetForUnity 설정

1. Unity 에디터를 실행합니다.  
2. 상단 메뉴의 `NuGet` -> `Manage NuGet Packages`를 선택합니다.  
3. 우측 상단의 검색창에 `RestSharp`을 입력하여 패키지를 검색합니다.

> [여기에 RestSharp 검색 결과 이미지를 삽입할 수 있습니다]

4. 검색된 결과 중 `RestSharp` 최신 버전을 선택하고, 다운로드(Install)를 진행합니다.  
5. 설치가 정상 완료되면 `Packages` 폴더나 `Packages/RestSharp` 경로에 라이브러리가 추가된 것을 확인할 수 있습니다.

---

## 예제 코드

다음은 RestSharp을 이용하여 간단하게 GET 요청을 보내는 예시입니다.

```csharp
using UnityEngine;
using RestSharp;
public class ExampleRestSharpUsage : MonoBehaviour
{
    // 기본 옵션 설정
    public static RestClientOptions options = new RestSharp.RestClientOptions(
        "https://jsonplaceholder.typicode.com"
    );

    // RestSharp 클라이언트 생성
    public static RestClient client = new RestClient(options);

    // 동기 예제 코드
    void Start()
    {
        // Endpoint 기반 RestRequest 생성
        var request = new RestRequest("posts");

        // 요청 실행
        var response = client.Get(request);

        // 응답 로그 출력
        Debug.Log($"Response Status: {response.StatusCode}");
        Debug.Log($"Response Content: {response.Content}");
    }

    // 비동기 예제 코드
    async void StartAsync()
    {
        // Endpoint 기반 RestRequest 생성
        var request = new RestRequest("posts");

        // 요청 실행
        var response = await client.GetAsync(request);

        // 응답 로그 출력
        Debug.Log($"Response Status: {response.StatusCode}");
        Debug.Log($"Response Content: {response.Content}");
    }
}
```

---

## 추가 정보 및 팁

### 공식 문서 참고

- [RestSharp 공식 문서](https://restsharp.dev/)를 참조하면 다양한 시나리오별 API 사용 방법을 자세히 확인할 수 있습니다.  
- NuGetForUnity에 대한 자세한 설정 및 문제 해결은 [NuGetForUnity 공식 저장소](https://github.com/GlitchEnzo/NuGetForUnity)를 참고하세요.

### Unity 환경에서의 주의사항

- Unity 버전에 따라 NuGetForUnity가 제대로 동작하지 않을 수 있습니다. 가능한 최신 LTS(Long-Term Support) 버전의 Unity를 사용하는 것을 권장합니다.  
- .NET 4.x 이상의 Scripting Runtime을 사용 중인지 확인해야 합니다. (Edit -> Project Settings -> Player -> Other Settings)

### 빌드 시 고려사항

- iOS, Android 등 모바일 플랫폼으로 빌드 시에도 NuGetForUnity와 RestSharp이 잘 동작하는지 사전에 확인하세요.  
- API 호환성(IL2CPP, .NET Standard 2.0 등)에 따라 문제가 발생할 수 있으므로, 테스트 빌드를 통해 정상 동작 여부를 점검하는 것이 좋습니다.

---

이상의 과정을 거치면 Unity 프로젝트에서 RestSharp을 자유롭게 사용하여 HTTP 통신 로직을 구현할 수 있습니다. 또한 필요에 따라 NuGetForUnity 설치 및 RestSharp 라이브러리 관리 과정을 반복하여, 다른 NuGet 패키지 역시 간편하게 사용할 수 있습니다.
