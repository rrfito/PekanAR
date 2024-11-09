package com.example.pekanar

import android.app.Activity
import android.content.Intent
import android.content.pm.PackageManager
import android.provider.Settings
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat

object CameraPermissionHelper {
    private const val CAMERA_PERMISSION_CODE = 0

    // Mengecek apakah izin kamera telah diberikan
    fun hasCameraPermission(activity: Activity): Boolean {
        return ContextCompat.checkSelfPermission(activity, android.Manifest.permission.CAMERA) == PackageManager.PERMISSION_GRANTED
    }

    // Meminta izin kamera jika belum diberikan
    fun requestCameraPermission(activity: Activity) {
        ActivityCompat.requestPermissions(activity, arrayOf(android.Manifest.permission.CAMERA), CAMERA_PERMISSION_CODE)
    }

    // Mengecek apakah pengguna perlu melihat alasan permintaan izin
    fun shouldShowRequestPermissionRationale(activity: Activity): Boolean {
        return ActivityCompat.shouldShowRequestPermissionRationale(activity, android.Manifest.permission.CAMERA)
    }

    // Meluncurkan pengaturan aplikasi untuk mengubah izin
    fun launchPermissionSettings(activity: Activity) {
        val intent = Intent(Settings.ACTION_APPLICATION_DETAILS_SETTINGS).apply {
            data = android.net.Uri.fromParts("package", activity.packageName, null)
        }
        activity.startActivity(intent)
    }
}
