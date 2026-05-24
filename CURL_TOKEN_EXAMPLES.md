# Ejemplos de uso del Token JWT para SACS API

## Endpoint de autenticación
- **URL**: `https://localhost:52165/auth/token`
- **Método**: POST
- **Respuesta**: `{ "access_token": "eyJhbGc..." }`
- **Expiración**: 1 hora

---

## 1. cURL (Linux/Mac/Git Bash)

### Obtener el token
```bash
curl -X POST "https://localhost:52165/auth/token" \
  -H "accept: */*" \
  -H "Content-Type: application/json"
```

### Guardar token en variable y usar en llamada
```bash
# Guardar el token
TOKEN=$(curl -s -X POST "https://localhost:52165/auth/token" | jq -r '.access_token')

# Usar el token para llamar a un endpoint protegido
curl -X GET "https://localhost:52165/api/asistencia?limit=5&offset=0" \
  -H "accept: */*" \
  -H "Authorization: Bearer $TOKEN"
```

### Ejemplo completo con endpoint específico
```bash
# Obtener token y llamar a planeamiento_didactico
TOKEN=$(curl -s -X POST "https://localhost:52165/auth/token" | jq -r '.access_token')

curl -X GET "https://localhost:52165/api/planeamiento_didactico?limit=50&offset=0&desc=false" \
  -H "Authorization: Bearer $TOKEN" \
  -H "accept: application/json"
```

---

## 2. PowerShell (Windows)

### Obtener el token
```powershell
$response = Invoke-RestMethod -Uri "https://localhost:52165/auth/token" -Method Post
$token = $response.access_token
Write-Host "Token obtenido: $token"
```

### Usar el token en una llamada
```powershell
# Obtener token
$response = Invoke-RestMethod -Uri "https://localhost:52165/auth/token" -Method Post
$token = $response.access_token

# Crear headers con el token
$headers = @{
    "Authorization" = "Bearer $token"
    "accept" = "application/json"
}

# Llamar a un endpoint protegido
$data = Invoke-RestMethod -Uri "https://localhost:52165/api/asistencia?limit=5" -Headers $headers
$data | ConvertTo-Json -Depth 10
```

### Script completo reutilizable
```powershell
# Script: Get-SacsData.ps1
function Get-SacsToken {
    $response = Invoke-RestMethod -Uri "https://localhost:52165/auth/token" -Method Post
    return $response.access_token
}

function Invoke-SacsApi {
    param(
        [string]$Endpoint,
        [hashtable]$QueryParams = @{}
    )
    
    $token = Get-SacsToken
    $headers = @{
        "Authorization" = "Bearer $token"
    }
    
    $queryString = ($QueryParams.GetEnumerator() | ForEach-Object { "$($_.Key)=$($_.Value)" }) -join "&"
    $url = "https://localhost:52165$Endpoint"
    if ($queryString) { $url += "?$queryString" }
    
    Invoke-RestMethod -Uri $url -Headers $headers
}

# Uso:
# $asistencias = Invoke-SacsApi -Endpoint "/api/asistencia" -QueryParams @{ limit=10; offset=0 }
# $periodos = Invoke-SacsApi -Endpoint "/api/periodos" -QueryParams @{ limit=50 }
```

---

## 3. JavaScript/TypeScript (Fetch API)

### Obtener token
```javascript
async function getToken() {
  const response = await fetch('https://localhost:52165/auth/token', {
    method: 'POST',
    headers: {
      'accept': '*/*'
    }
  });
  const data = await response.json();
  return data.access_token;
}
```

### Usar el token
```javascript
async function fetchAsistencias() {
  // Obtener el token
  const tokenResponse = await fetch('https://localhost:52165/auth/token', {
    method: 'POST'
  });
  const { access_token } = await tokenResponse.json();

  // Usar el token para llamar al endpoint
  const dataResponse = await fetch('https://localhost:52165/api/asistencia?limit=5', {
    headers: {
      'Authorization': `Bearer ${access_token}`,
      'accept': 'application/json'
    }
  });
  
  const data = await dataResponse.json();
  return data;
}

// Uso
fetchAsistencias().then(data => console.log(data));
```

### Clase reutilizable
```javascript
class SacsApiClient {
  constructor(baseUrl = 'https://localhost:52165') {
    this.baseUrl = baseUrl;
    this.token = null;
  }

  async getToken() {
    const response = await fetch(`${this.baseUrl}/auth/token`, {
      method: 'POST'
    });
    const data = await response.json();
    this.token = data.access_token;
    return this.token;
  }

  async request(endpoint, options = {}) {
    if (!this.token) {
      await this.getToken();
    }

    const response = await fetch(`${this.baseUrl}${endpoint}`, {
      ...options,
      headers: {
        'Authorization': `Bearer ${this.token}`,
        'Content-Type': 'application/json',
        ...options.headers
      }
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    return await response.json();
  }

  async getAsistencias(limit = 50, offset = 0) {
    return this.request(`/api/asistencia?limit=${limit}&offset=${offset}`);
  }

  async getPeriodos(limit = 50, offset = 0) {
    return this.request(`/api/periodos?limit=${limit}&offset=${offset}`);
  }
}

// Uso:
// const client = new SacsApiClient();
// const asistencias = await client.getAsistencias(10, 0);
```

---

## 4. Python (requests)

### Obtener token
```python
import requests

def get_token():
    url = "https://localhost:52165/auth/token"
    response = requests.post(url, verify=False)  # verify=False para desarrollo con certificado autofirmado
    return response.json()['access_token']

token = get_token()
print(f"Token: {token}")
```

### Usar el token
```python
import requests

# Obtener token
token_url = "https://localhost:52165/auth/token"
token_response = requests.post(token_url, verify=False)
token = token_response.json()['access_token']

# Usar el token
headers = {
    'Authorization': f'Bearer {token}',
    'accept': 'application/json'
}

api_url = "https://localhost:52165/api/asistencia"
params = {'limit': 5, 'offset': 0}

data_response = requests.get(api_url, headers=headers, params=params, verify=False)
data = data_response.json()

print(data)
```

### Clase reutilizable
```python
import requests
from typing import Optional, Dict, Any

class SacsApiClient:
    def __init__(self, base_url: str = "https://localhost:52165"):
        self.base_url = base_url
        self.token: Optional[str] = None
        self.session = requests.Session()
        self.session.verify = False  # Para desarrollo con certificado autofirmado

    def get_token(self) -> str:
        """Obtiene un nuevo token JWT"""
        url = f"{self.base_url}/auth/token"
        response = self.session.post(url)
        response.raise_for_status()
        self.token = response.json()['access_token']
        return self.token

    def request(self, endpoint: str, method: str = 'GET', **kwargs) -> Dict[Any, Any]:
        """Realiza una petición autenticada"""
        if not self.token:
            self.get_token()
        
        headers = kwargs.pop('headers', {})
        headers['Authorization'] = f'Bearer {self.token}'
        
        url = f"{self.base_url}{endpoint}"
        response = self.session.request(method, url, headers=headers, **kwargs)
        response.raise_for_status()
        return response.json()

    def get_asistencias(self, limit: int = 50, offset: int = 0) -> Dict[Any, Any]:
        """Obtiene registros de asistencia"""
        params = {'limit': limit, 'offset': offset}
        return self.request('/api/asistencia', params=params)

    def get_periodos(self, limit: int = 50, offset: int = 0) -> Dict[Any, Any]:
        """Obtiene registros de periodos"""
        params = {'limit': limit, 'offset': offset}
        return self.request('/api/periodos', params=params)

# Uso:
# client = SacsApiClient()
# asistencias = client.get_asistencias(limit=10)
# print(asistencias)
```

---

## 5. C# (.NET)

### HttpClient simple
```csharp
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

// Obtener token
var httpClient = new HttpClient();
var tokenResponse = await httpClient.PostAsync("https://localhost:52165/auth/token", null);
var tokenData = await tokenResponse.Content.ReadFromJsonAsync<JsonElement>();
var token = tokenData.GetProperty("access_token").GetString();

// Usar el token
httpClient.DefaultRequestHeaders.Authorization = 
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

var dataResponse = await httpClient.GetAsync("https://localhost:52165/api/asistencia?limit=5");
var data = await dataResponse.Content.ReadAsStringAsync();
Console.WriteLine(data);
```

### Clase reutilizable
```csharp
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

public class SacsApiClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private string? _token;

    public SacsApiClient(string baseUrl = "https://localhost:52165")
    {
        _baseUrl = baseUrl;
        _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    public async Task<string> GetTokenAsync()
    {
        var response = await _httpClient.PostAsync("/auth/token", null);
        response.EnsureSuccessStatusCode();
        
        var data = await response.Content.ReadFromJsonAsync<JsonElement>();
        _token = data.GetProperty("access_token").GetString();
        
        _httpClient.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
        
        return _token;
    }

    public async Task<T> GetAsync<T>(string endpoint, Dictionary<string, string>? queryParams = null)
    {
        if (string.IsNullOrEmpty(_token))
            await GetTokenAsync();

        var url = endpoint;
        if (queryParams != null && queryParams.Any())
        {
            var query = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            url += $"?{query}";
        }

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}

// Uso:
// using var client = new SacsApiClient();
// var asistencias = await client.GetAsync<dynamic>("/api/asistencia", new() { ["limit"] = "10" });
```

---

## 6. Postman

1. **Crear una colección nueva**
2. **Agregar variable de entorno**:
   - Variable: `base_url`
   - Valor: `https://localhost:52165`

3. **Request para obtener token**:
   - Método: `POST`
   - URL: `{{base_url}}/auth/token`
   - Headers: `accept: */*`
   - En la pestaña **Tests**, agregar:
   ```javascript
   var jsonData = pm.response.json();
   pm.environment.set("token", jsonData.access_token);
   ```

4. **Request para usar el token**:
   - Método: `GET`
   - URL: `{{base_url}}/api/asistencia?limit=5`
   - En la pestaña **Authorization**:
     - Type: `Bearer Token`
     - Token: `{{token}}`

---

## Configuración JWT (appsettings.json)

```json
{
  "Jwt": {
    "Issuer": "SACS.Api",
    "Audience": "SACS.Client",
    "Key": "CHANGE_ME_32CHARS_MIN_SECRET_KEY",
    "ExpiresMinutes": 120
  }
}
```

- **Issuer**: Emisor del token
- **Audience**: Audiencia esperada
- **Key**: Clave secreta (mínimo 32 caracteres en producción)
- **ExpiresMinutes**: No se usa actualmente (el código tiene hardcoded 1 hora)

---

## Notas importantes

1. **Certificado SSL**: En desarrollo, puede ser necesario:
   - cURL: Usar `-k` o `--insecure`
   - Python: `verify=False`
   - .NET: Configurar `HttpClientHandler` para aceptar certificados autofirmados

2. **CORS**: La API acepta peticiones desde `http://localhost:5173` (configurado en appsettings.json)

3. **Expiración**: El token expira en 1 hora. Después debe obtenerse uno nuevo.

4. **Seguridad**: 
   - En producción, cambiar la clave JWT a una más segura
   - Usar HTTPS siempre
   - Implementar refresh tokens para mejor UX
