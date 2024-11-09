package com.example.pekanar

import android.content.ContentValues.TAG
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import android.view.View
import com.google.ar.core.ArCoreApk
import com.google.ar.core.Config
import com.google.ar.core.Session
import com.google.ar.core.exceptions.UnavailableException
import com.google.ar.core.exceptions.UnavailableUserDeclinedInstallationException

class ArrActivity : AppCompatActivity() {

    // Variabel untuk menyimpan sesi ARCore
    private var mSession: Session? = null




    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge() // Mengaktifkan tampilan layar penuh (edge-to-edge)
        setContentView(R.layout.activity_arr)

        // Atur tampilan layar penuh dengan insets
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }
        if (isARCoreSupportedAndUpToDate()) {
            createSession()
            Log.d("sesssionnn","session berhasil dibuat")
        } else {
            // Tampilkan pesan jika ARCore tidak didukung atau perlu diperbarui
            Toast.makeText(this, "ARCore is not supported or needs an update.", Toast.LENGTH_LONG).show()
            finish()
        }


    }

    override fun onResume() {
        super.onResume()

        // Memeriksa izin kamera sebelum menginisialisasi sesi AR
        if (!CameraPermissionHelper.hasCameraPermission(this)) {
            CameraPermissionHelper.requestCameraPermission(this)
            return
        }

        }




    // Fungsi untuk memeriksa apakah ARCore didukung dan versi yang digunakan adalah versi terbaru
    fun isARCoreSupportedAndUpToDate(): Boolean {
        return when (ArCoreApk.getInstance().checkAvailability(this)) {
            ArCoreApk.Availability.SUPPORTED_INSTALLED -> true
            ArCoreApk.Availability.SUPPORTED_APK_TOO_OLD, ArCoreApk.Availability.SUPPORTED_NOT_INSTALLED -> {
                try {
                    // Meminta instalasi atau pembaruan ARCore jika diperlukan
                    when (ArCoreApk.getInstance().requestInstall(this, true)) {
                        ArCoreApk.InstallStatus.INSTALL_REQUESTED -> {
                            Log.i(TAG, "ARCore installation requested.")
                            false
                        }
                        ArCoreApk.InstallStatus.INSTALLED -> true
                    }
                } catch (e: UnavailableException) {
                    Log.e(TAG, "ARCore not installed", e)
                    false
                }
            }
            ArCoreApk.Availability.UNSUPPORTED_DEVICE_NOT_CAPABLE -> false // Perangkat tidak mendukung AR
            else -> false // Kembali false untuk kasus kesalahan atau pengecekan yang belum selesai
        }
    }

    // Fungsi untuk membuat sesi ARCore
    fun createSession() {
        // Membuat sesi ARCore
        mSession = Session(this)

        // Membuat konfigurasi sesi ARCore
        val config = Config(mSession)

        // Lakukan konfigurasi yang spesifik untuk fitur, seperti menyalakan dukungan depth
        // atau menyalakan dukungan untuk wajah Augmented.

        // Mengonfigurasi sesi AR dengan konfigurasi yang telah dibuat
        mSession?.configure(config)
    }

    override fun onPause() {
        super.onPause()
        mSession?.close()
        Log.d("cyclessss","onPause : ")
    }

    override fun onDestroy() {
        super.onDestroy()
        // Menutup sesi ARCore saat aktivitas dihancurkan
        mSession?.close()
        Log.d("cyclessss","destroy : ")
    }

    // Penanganan hasil permintaan izin kamera
    override fun onRequestPermissionsResult(
        requestCode: Int,
        permissions: Array<String>,
        results: IntArray
    ) {
        super.onRequestPermissionsResult(requestCode, permissions, results)
        if (!CameraPermissionHelper.hasCameraPermission(this)) {
            // Tampilkan pesan jika izin kamera tidak diberikan
            Toast.makeText(this, "Camera permission is needed to run this application", Toast.LENGTH_LONG).show()
            if (!CameraPermissionHelper.shouldShowRequestPermissionRationale(this)) {
                // Jika izin ditolak permanen, arahkan pengguna ke pengaturan untuk mengizinkan
                CameraPermissionHelper.launchPermissionSettings(this)
            }
            finish() // Akhiri aktivitas jika izin kamera tidak diberikan
        }
    }
}
